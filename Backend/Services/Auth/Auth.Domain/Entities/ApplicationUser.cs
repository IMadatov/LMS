using BaseCrud.Entities;
using General.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entities;

public class ApplicationUser: IdentityUser<Guid>,IEntity<Guid>
{
    [NotMapped]
    public string FullName => $"{LastName} {FirstName}";
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? TelegramId { get; set; }

    public string? PhotoUrl { get; set; }

    public string? Hash { get; set; }

    public string? AuthDate { get; set; }

    public string? TelegramUserName { get; set; }

    public StatusUser? StatusUser { get; set; }

    public bool Active { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public Languages Language { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public string? MainRole { get; set; }
}
