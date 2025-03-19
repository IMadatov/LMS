using BaseCrud.ServiceResults;
using Auth.Domain.Entities;
using Auth.Domain.DTOs;


namespace Auth.Application.Services;

public interface IAuthService
{
    Task<ServiceResult<bool>> SignUpAsync(SignUpDto signUpDto);
    Task<ServiceResult<bool>> SignOutAsync();
    Task<ServiceResult<bool>> SignInAsync(SignInDto signInDto);
    Task<ServiceResult<string>> OnSite();
    Task<ServiceResult<bool>> SignUpWithTelegram(UserTeleramDTO userTeleramDTO,string telegramData);
    Task<ServiceResult<bool>> SignInWithTelegram(UserTelegram userTelegram);
    Task<ServiceResult<bool>> CheckUsername(string username);
    Task<ServiceResult<bool>> CheckTelegramData(string telegramData);
    Task<ServiceResult<Guid>> CreateRole(string roleName);
}
