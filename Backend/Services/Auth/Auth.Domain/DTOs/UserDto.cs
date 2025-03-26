using Auth.Domain.Entities;
using BaseCrud.Entities;
using General.DTOs;
using General.Enums;

namespace Auth.Domain.DTOs
{
    public class UserDto:IDataTransferObject<ApplicationUser,Guid>
    {
        public Guid Id { get; set; }
        public string? TelegramId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public string? TelegramUserName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public Languages Language { get; set; }

        public string strLanguage
        {
            get
            {
                string lang = "en";
                switch (Language.ToString())
                {
                    case "UZBEK":
                        return "uz";
                    case "RUSSIAN":
                        return "ru";
                    case "QARAQALPAQ":
                        return "kr";
                    case "ENGLISH":
                        return "en";
                }

                return lang;
            }
        }
        public bool Active { get; set; }

        public StatusUserDto? StatusUser { get; set; }

        public IList<string> Roles { get; set; } = [];
    }
}
