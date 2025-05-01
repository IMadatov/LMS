using BaseCrud.ServiceResults;
using Auth.Domain.Entities;
using Auth.Domain.DTOs;


namespace Auth.Application.Services;

public interface IAuthService
{
    //Sign UP
    Task<ServiceResult<bool>> SignUpWithTelegram(UserTeleramDTO userTeleramDTO,string telegramData);



    //Sign In
    Task<ServiceResult<JWTTokenModel>> SignInAsync(SignInDto signInDto);
    Task<ServiceResult<JWTTokenModel>> SignInWithTelegram(UserTelegram userTelegram);

    //Sign Out
    Task<ServiceResult<bool>> SignOutAsync();

    //Token
    Task<ServiceResult<JWTTokenModel>> RefreshToken(JWTTokenModel model);

    //Checks
    Task<ServiceResult<string>> OnSite();
    Task<ServiceResult<bool>> CheckUsername(string username);
    Task<ServiceResult<bool>> CheckTelegramData(string telegramData);

    //Change Role
    Task<ServiceResult<JWTTokenModel>> ChangeMainRole(string UserId,string ToRole);

    Task<List<ApplicationRole>> GetRoles();
}
