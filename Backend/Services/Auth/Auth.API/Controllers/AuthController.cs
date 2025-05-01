using Auth.Application.Services;
using Auth.Domain.DTOs;
using Auth.Domain.Entities;
using Clients.TelegramBot.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Auth.API.Controllers;

[AllowAnonymous]
public class AuthController(
    IAuthService authService,
    IUserService userService,
    TelegramBotClient telegramBotClient
    ) : BaseController
{
    //[HttpPost]
    //public async Task<ActionResult<bool>> SignOut() =>
    //    await FromServiceResult(authService.SignOutAsync());

    [HttpPost]
    public async Task<ActionResult<JWTTokenModel?>> SignInAsync(SignInDto signInDto)
        => await FromServiceResult(authService.SignInAsync(signInDto));

    [HttpPost]
    public async Task<ActionResult<JWTTokenModel?>> RefreshToken(JWTTokenModel model)
        => await FromServiceResult(authService.RefreshToken(model));

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<string?>> OnSite()
        => await FromServiceResult(authService.OnSite());


    [HttpPost]
    public async Task<ActionResult<bool>> SignUpWithTelegram(UserTeleramDTO userTelegram, string telegramData)
        => await FromServiceResult(authService.SignUpWithTelegram(userTelegram, telegramData));



    [HttpPost]
    public async Task<ActionResult<bool>> CheckUser(string userTelegramData)
        => await FromServiceResult(authService.CheckUsername(userTelegramData));

    [HttpGet]
    public async Task<ActionResult<bool>> CheckUsername(string username)
        => await FromServiceResult(authService.CheckUsername(username));

    [HttpPost]
    public async Task<ActionResult<bool>> CheckTelegramData(string telegramData)
        => await FromServiceResult(authService.CheckTelegramData(telegramData));

    [HttpPost]
    public async Task<ActionResult> GetUserAsTelegramBot(string id)
    {
        try
        {
            var user = await telegramBotClient.GetUserInfoAsync(id);

            

            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<JWTTokenModel?>> ChangeRole(string UserId,string ToRole) =>
        await FromServiceResult(authService.ChangeMainRole(UserId,ToRole));

    [HttpGet]
    [Authorize(Roles= "admin")]
    public async Task<ActionResult<List<ApplicationRole>>> GetRoles()
    {
        return await authService.GetRoles();
    } 
}
