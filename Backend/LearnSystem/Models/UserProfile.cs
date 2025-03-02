using BaseCrud.Abstractions.Entities;
using Domain.Models;

namespace LearnSystem.Models
{
    public class UserProfile : IUserProfile<Guid>
    {
        public string? UserName { get ; set ; }
        public string? Fullname { get ; set ; }
        public Guid Id { get ; set ; }
        public Languages Language { get; set; }
    }
}
