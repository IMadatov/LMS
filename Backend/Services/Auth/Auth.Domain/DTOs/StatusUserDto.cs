using Auth.Domain.Entities;
using BaseCrud.Entities;

namespace General.DTOs;

public class StatusUserDto:IDataTransferObject<StatusUser,Guid>
{
    public Guid Id { get; set; }
    public bool IsOnTelegramBotActive { get; set; }

    public bool HasPhotoProfile { get; set; }
}
