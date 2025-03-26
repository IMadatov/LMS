
using Auth.Domain.DTOs;
using Auth.Domain.Entities;
using BaseCrud.Abstractions.Entities;
using BaseCrud.EntityFrameworkCore.Services;
using BaseCrud.ServiceResults;
using General.DTOs;
namespace Auth.Application.Services
{
    public interface IUserService: IEfCrudService<ApplicationUser,UserDto,UserDto,Guid,Guid>
    {
        Task<ServiceResult<LanguageDto>> UserLanguage(IUserProfile<Guid> userProfile);
        Task<ServiceResult<LanguageDto>> ChangeLanguage(string lang, IUserProfile<Guid> userProfile);
        Task<ServiceResult<bool>> Refresh();
        //Task<ServiceResult<bool>> Init();
    }
}
