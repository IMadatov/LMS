using BaseCrud.Abstractions.Entities;
using LearnSystem.Models;
using LearnSystem.Models.ModelsDTO;
using ServiceStatusResult;


namespace LearnSystem.Services.IServices
{

    public interface IUserService
    {
        Task<ServiceResultBase<UserDto>> Me(Guid userId);
        Task<ServiceResultBase<LanguageDto>> UserLanguage(IUserProfile<Guid> userProfile);

        Task<ServiceResultBase<LanguageDto>> ChangeLanguage(string lang,IUserProfile<Guid> userProfile);
        Task<ServiceResultBase<bool>> Refresh();
    }
}