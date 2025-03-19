using BaseCrud.Abstractions.Entities;

namespace Auth.Domain.Entities;

public class StatusUser:EntityBase<Guid>
{
    public bool IsOnTelegramBotActive { get; set; }

    public bool HasPhotoProfile { get; set; }

    public ApplicationUser ApplicationUser { get; set; }
    public Guid ApplicationUserId { get; set; }
}
