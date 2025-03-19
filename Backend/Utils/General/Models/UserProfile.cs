using BaseCrud.Abstractions.Entities;
using General.Enums;

namespace General.Models;

public class UserProfile : IUserProfile<Guid>
{
    public string? UserName { get; set; }
    public string? Fullname { get; set; }
    public Guid Id { get; set; }
    public string? Role { get; set; }
    public Languages Language { get; set; }
}
