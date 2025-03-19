using BaseCrud.ServiceResults;
using General.DTOs;

namespace TelegramBot.Application.Services
{
    public interface ITelegramBotService
    {
        Task<ServiceResult<TelegramChatDto>> GetUserInfo(string telegramId);

        Task<ServiceResult<bool>> SendMessage(string telegramId, string message);


    }
}
