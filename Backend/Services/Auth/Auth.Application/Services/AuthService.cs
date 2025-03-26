using Auth.Domain.DTOs;
using Auth.Domain.Entities;
using Auth.Infrastructure.Context;
using BaseCrud.Errors;
using BaseCrud.ServiceResults;
using General.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application.Services;

public class AuthService(
    AuthContext _dbContext,
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    RoleManager<ApplicationRole> roleManager
    ) : IAuthService
{
    /// <summary>
    /// SignUp with Telegram
    /// </summary>
    /// <param name="userTeleramDTO"></param>
    /// <param name="telegramData"></param>
    /// <returns></returns>
    public async Task<ServiceResult<bool>> SignUpWithTelegram(UserTeleramDTO userTeleramDTO, string telegramData)
    {

        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(telegramData);

        var userTelegram = Newtonsoft.Json.JsonConvert.DeserializeObject<UserTelegram>(telegramData);

        if (!await CheckAuthorizeFromBot(dictionary))
            return ServiceResult.BadRequest(
                new ServiceError("You should be authorized from bot", "unauthorized")
                );

        var register = await userManager.Users.FirstOrDefaultAsync(x => x.TelegramId == userTelegram.id);

        if (register != null)
            return ServiceResult.BadRequest(
                new ServiceError("this user is registered", "signin")
                );

        //var client = new HttpClient({ BaseAddress=""});

        //if (!await telegramService.SendMassage(long.Parse(userTelegram.id), "Hi. User of LMS. I\'m bot for learning system. You should not block the bot. If you block the bot, you won't be able to log in  "))
        //{
        //    return ServiceResult.BadRequest(new ServiceError("You don't acceptad", "telegram_bot"));
        //}


        var status = new StatusUser
        {
            HasPhotoProfile = false,
            IsOnTelegramBotActive = true
        };

        _dbContext.StatusUsers.Add(status);



        var user = new ApplicationUser
        {
            UserName = userTeleramDTO.UserName,
            Email = userTeleramDTO.Email,
            FirstName = userTelegram.first_name,
            LastName = userTelegram.last_name,
            AuthDate = userTelegram.auth_date,
            TelegramId = userTelegram.id,
            Hash = userTelegram.hash,
            TelegramUserName = userTelegram.username,
            PhotoUrl = userTelegram.photo_url,
            StatusUser = status,
            CreatedDate = DateTime.Now,
            Language = Languages.ENGLISH
        };





        var result = await userManager.CreateAsync(user, userTeleramDTO.Password);
        var countUser = _dbContext.Users.Count();
        if (result.Succeeded)
        {



            if (countUser < 4)
            {
                var rolesFromDb = await _dbContext.Roles.ToListAsync();


                if (rolesFromDb.Count == 0)
                {
                    var roles = new List<ApplicationRole> {
                        new ApplicationRole { Name="admin" },
                        new ApplicationRole { Name = "student" },
                        new ApplicationRole { Name = "teacher" }
                    };

                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(role);
                    }
                }





                await userManager.AddToRoleAsync(user, "admin");
            }

            else
                await userManager.AddToRoleAsync(user, "student");

            await signInManager.SignInAsync(user, true);
            //var token = await userManager.CreateSecurityTokenAsync(user);

            //var loginInfoUser = new UserLoginInfo("Telegram",user.Hash, "");

            //var res = await userManager.AddLoginAsync(user, loginInfoUser);


            return ServiceResult.Ok(true);
        }


        return ServiceResult.BadRequest(
            new ServiceError(
                string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)),
                "signup.telegram"
                )
            );

    }

    /// <summary>
    /// SignIn with username and password
    /// </summary>
    /// <param name="signInDto"></param>
    /// <returns></returns>
    public async Task<ServiceResult<JWTTokenModel>> SignInAsync(SignInDto signInDto)
    {
        var result = await signInManager.PasswordSignInAsync(signInDto.Username, signInDto.Password, signInDto.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync(signInDto.Username);

            var roles = await userManager.GetRolesAsync(user);

            return ServiceResult.Ok(await GenerateTokenAsync(user,roles.ToList(),signInDto.RememberMe));

        }
        
        return ServiceResult.BadRequest(new ServiceError("Password or Username are invalid","password_username_invalid"));
    }

    ///<summary>
    ///SignIn with Telegram Widget
    /// </summary>   
    public async Task<ServiceResult<JWTTokenModel>> SignInWithTelegram(UserTelegram userTelegram)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.TelegramId == userTelegram.id);

        if(user is null)
        {
            return ServiceResult.NotFound();
        }

        var roles = await userManager.GetRolesAsync(user);

        await signInManager.SignInAsync(user, true);

        var token =await GenerateTokenAsync(user, roles.ToList(), true);

        return ServiceResult.Ok(token);
    }
    

    public async Task<ServiceResult<bool>> SignOutAsync()
    {
        await signInManager.SignOutAsync();
        var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        if (userId is null)
            return ServiceResult.Ok(true);

        var user = await userManager.FindByIdAsync(userId);

        if (user is not null)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await userManager.UpdateAsync(user);
        }

        return ServiceResult.Ok(true);
    }

    /// <summary>
    /// Token Refresh
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ServiceResult<JWTTokenModel>> RefreshToken(JWTTokenModel model)
    {
        if (model is null)
            return ServiceResult.BadRequest(new ServiceError("Refresh token is null", "refresh_token"));

        var principal = GetPrincipalFromExpiredToken(model.AccessToken);

        if (principal.Identity.Name is null)
            return ServiceResult.BadRequest(new ServiceError("Invalid access token or refresh token", "refresh_token"));

        var user = await userManager.FindByNameAsync(principal.Identity.Name);

        if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return ServiceResult.BadRequest(new ServiceError("Invalid access token or refresh token", "refresh_token"));

        var isPersistent = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.IsPersistent)?.Value=="1"?true:false;


        var roles = await userManager.GetRolesAsync(user);

        return ServiceResult.Ok(await GenerateTokenAsync(user,roles.ToList(),isPersistent));
    }


    /// <summary>
    /// Check user is on site
    /// </summary>
    /// <returns></returns>
    public async Task<ServiceResult<string?>> OnSite()
    {
        var isAuth = httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated;
        if (isAuth is null || isAuth == false)
            return ServiceResult.Ok("false");
        var expiration = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration).Value;

        if (DateTime.Parse(expiration) <= DateTime.Now || expiration == null)
            return ServiceResult.Unauthorized(new ServiceError("Token is invalid", "token_invalid"));

        var currentRole = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        if (currentRole != null)
            return ServiceResult.Ok(currentRole);
        return ServiceResult.NoContent();
    }

    public async Task<ServiceResult<bool>> CheckTelegramData(string telegramData)
    {
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(telegramData);
        if (!await CheckAuthorizeFromBot(dictionary))
            return ServiceResult.Ok(false);

        UserTelegram userTelegram = JsonConvert.DeserializeObject<UserTelegram>(telegramData);

        var user = await userManager.Users.FirstOrDefaultAsync(x => x.TelegramId == userTelegram!.id);

        if (user == null)
            return ServiceResult.Unauthorized(new ServiceError("You should be SignUp", "SignUp"));


        await signInManager.SignInAsync(user, true);


        return ServiceResult.Ok(true);

    }

    public async Task<ServiceResult<bool>> CheckUsername(string username)
    {
        var result = await userManager.FindByNameAsync(username);

        if (result == null)
            return ServiceResult.Ok(false);

        return ServiceResult.Ok(true);
    }

    public async Task<ServiceResult<Guid>> CreateRole(string roleName)
    {

        var role = new ApplicationRole
        {
            Name = roleName
        };
        await roleManager.CreateAsync(role);

        return ServiceResult.Ok(role.Id);
    }

    public async Task<ServiceResult<JWTTokenModel>> ChangeMainRole(string userId, string ToRole)
    {
        

        if (userId is null)
            return ServiceResult.Unauthorized();

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return ServiceResult.BadRequest();

        var role = await roleManager.FindByNameAsync(ToRole);

        if (role is null)
            return ServiceResult.BadRequest(new ServiceError
                ($"{ToRole} not found", "role_not_found"));


        var roleExsist = await userManager.IsInRoleAsync(user, role.Name);

        if (!roleExsist)
            return ServiceResult.BadRequest(
                new ServiceError("Access danied for that role: " + ToRole, "access-danied")
                );

        user.MainRole = role.Name;

        await userManager.UpdateAsync(user);

        var roles= await userManager.GetRolesAsync(user);

        var isPersistent = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.IsPersistent)?.Value == "1" ? true : false;

        var token = await GenerateTokenAsync(user, roles.ToList(), isPersistent);

        return ServiceResult.Ok(token);
    }


    //Checks private methods
    async Task<bool> CheckAuthorizeFromBot(Dictionary<string, string> userTelegram)
    {

        var botToken = "6808715662:AAG1YaD5SU1824vcas2N-b2q0TF3XOP4npU";

        using var sha256 = SHA256.Create();

        var secret = sha256.ComputeHash(Encoding.UTF8.GetBytes(botToken));

        var array = userTelegram
            .Where(k => k.Key != "hash")
            .Select(k => $"{k.Key}={k.Value}")
            .ToList();

        array.Sort();

        var sortedData = string.Join("\n", array);

        using var hmac = new HMACSHA256(secret);

        var checkHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(sortedData));

        var checkHashHex = BitConverter.ToString(checkHash).Replace("-", "").ToLower();

        return checkHashHex == userTelegram["hash"];

    }
    async Task<JWTTokenModel> GenerateTokenAsync(ApplicationUser user, List<string> roles, bool RememberMe)
    {
        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.IsPersistent,RememberMe?"1":"0"),
                    new Claim(ClaimTypes.Role,user.MainRole??"user"),
                    new Claim(ClaimTypes.Expiration,DateTime.Now.AddHours(2).ToString()),
                    new Claim(ClaimTypes.Country,user.Language.ToString()),
                };
        foreach (var role in roles)
        {
            claims.Add(new Claim("roles", role));
        }


        var accessToken = GenerateAccessToken(claims);
        var refreshToken = await GenerateRefreshTokenAsync(claims);


        return new JWTTokenModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            RefreshToken = refreshToken
        };
    }
    async Task<string> GenerateRefreshTokenAsync(List<Claim> claims)
    {
        var randomNumber = new byte[64];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);

        var refreshToken = Convert.ToBase64String(randomNumber);

        var user = await userManager.FindByIdAsync(claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

        var isPersistent = claims.FirstOrDefault(x => x.Type == ClaimTypes.IsPersistent)?.Value;

        user.RefreshToken = refreshToken;

        user.RefreshTokenExpiryTime = isPersistent == "1" ? DateTime.Now.AddDays(5) : DateTime.Now.AddHours(2.5);

        var result = await userManager.UpdateAsync(user);

        return refreshToken;
    }
    JwtSecurityToken GenerateAccessToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));

        var expiration = DateTime.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration).Value);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            expires: expiration,
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokentValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokentValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }

}