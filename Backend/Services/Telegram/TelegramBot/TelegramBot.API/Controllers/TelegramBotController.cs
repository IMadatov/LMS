

using General.DTOs;
using Microsoft.AspNetCore.Mvc;
using TelegramBot.Application.Services;

namespace TelegramBot.API.Controllers;

public class TelegramBotController(
    ITelegramBotService service) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<TelegramChatDto?>> GetUserInfo(string telegramId)
    {
        var httpContext = HttpContext;
        var user = User.Identity.Name;
        return await FromServiceResult(service.GetUserInfo(telegramId));
    }
}
