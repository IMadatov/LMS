using BaseCrud.Errors;
using BaseCrud.ServiceResults;
using General.DTOs;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Application.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly IConfiguration _configuration;
        private readonly TelegramBotClient _bot;

        public TelegramBotService(
            IConfiguration configuration
            )
        {
            _configuration = configuration;

            _bot = new TelegramBotClient(_configuration.GetSection("Telegram:TokenBot").Value);
        }

        public async Task<ServiceResult<TelegramChatDto>> GetUserInfo(string telegramId)
        {

            try
            {
                var chatId = new ChatId(long.Parse(telegramId));
                
                var chat = await _bot.GetChatAsync(chatId);

                var dto = new TelegramChatDto
                {
                    FirstName = chat.FirstName,
                    LastName = chat.LastName,
                    Id=chat.Id,
                    Username=chat.Username
                };

                return ServiceResult.Ok(dto);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ServiceResult.BadRequest(new ServiceError("Telegram chat not found","GetUserInfo"));
        }

        public async Task<ServiceResult<bool>> SendMessage(string telegramId, string message)
        {
            try
            {
                var chatId = new ChatId(long.Parse(telegramId));

                var messager = await _bot.SendTextMessageAsync(chatId, message);
                return ServiceResult.Ok(true);
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ServiceResult.Ok(false);
        }


    }
}
