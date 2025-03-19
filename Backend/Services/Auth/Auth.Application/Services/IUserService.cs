using BaseCrud.Abstractions.Entities;
using BaseCrud.ServiceResults;
using General.DTOs;

namespace Auth.Application.Services
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> Me(IUserProfile<Guid> userProfile);
        Task<ServiceResult<LanguageDto>> UserLanguage(IUserProfile<Guid> userProfile);
        Task<ServiceResult<LanguageDto>> ChangeLanguage(string lang, IUserProfile<Guid> userProfile);
        Task<ServiceResult<bool>> Refresh();
        Task<ServiceResult<bool>> Init();
    }
}
