using Auth.Domain.DTOs;
using Auth.Domain.Entities;
using Auth.Infrastructure.Context;
using BaseCrud.Errors;
using BaseCrud.Errors.Keys;
using BaseCrud.ServiceResults;
using General.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
    //ITelegramService telegramService,
    RoleManager<ApplicationRole> roleManager
    ) : IAuthService
{
    public async Task<ServiceResult<bool>> SignUpAsync(SignUpDto signUpDto)
    {
        var user = new ApplicationUser
        {
            Email = signUpDto.Email,
            FirstName = signUpDto.FirstName,
            LastName = signUpDto.LastName,
            UserName = signUpDto.UserName,
            CreatedDate = DateTime.Now
        };

        var result = await userManager.CreateAsync(user, signUpDto.Password!);

        if (result.Succeeded)

            return ServiceResult.Ok(true);

        
        return ServiceResult.BadRequest(
            new ServiceError(
                string.Join(
                    Environment.NewLine,
                    result.Errors.Select(x=>x.Description)
                    ),
                string.Join(
                    Environment.NewLine,
                    result.Errors.Select(x => x.Code)
                    )
                )
            ); 
    }

    public async Task<ServiceResult<bool>> SignOutAsync()
    {
        await signInManager.SignOutAsync();
        return ServiceResult.Ok(true);
    }

    public async Task<ServiceResult<bool>> SignInAsync(SignInDto signInDto)
    {


        try
        {
            var result = await signInManager.PasswordSignInAsync(signInDto.Username, signInDto.Password, signInDto.RememberMe, lockoutOnFailure: true);



            if (result.Succeeded)
            {

                var user = await userManager.FindByNameAsync(signInDto.Username);
                var roles = await userManager.GetRolesAsync(user);
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Country, user.Language.ToString()));

                return ServiceResult.Ok(true);
            }

            return ServiceResult.Ok(false);
        }
        catch (Exception e)
        {

            return ServiceResult.BadRequest(new ServiceError("21", ErrorKeys.Database.EntityDeactivated));
        }
    }



    public async Task<ServiceResult<string>> OnSite()
    {
        var userId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;



        if (userId == null)
        {
            return ServiceResult.Ok("false");
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return ServiceResult.Ok("false");
        }


        var role = await userManager.GetRolesAsync(user);

        return ServiceResult.Ok(role.FirstOrDefault().ToString());
    }


    public async Task<ServiceResult<bool>> SignInWithTelegram(UserTelegram userTelegram)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.TelegramId == userTelegram.id);

        await signInManager.SignInAsync(user, true);

        //var result = await signInManager.SignInAsync()
        return ServiceResult.Ok(true);
    }


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

    public async Task<ServiceResult<bool>> CheckTelegramData(string telegramData)
    {
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(telegramData);
        if (!await CheckAuthorizeFromBot(dictionary))
            return ServiceResult.Ok(false); 

        UserTelegram userTelegram = JsonConvert.DeserializeObject<UserTelegram>(telegramData);

        var user = await userManager.Users.FirstOrDefaultAsync(x => x.TelegramId == userTelegram!.id);

        if (user == null)
            return ServiceResult.Unauthorized(new ServiceError("You should be SignUp","SignUp"));


        await signInManager.SignInAsync(user, true);


        return ServiceResult.Ok(true);

    }

    private async Task<bool> CheckAuthorizeFromBot(Dictionary<string, string> userTelegram)
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
}


