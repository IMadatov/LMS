
using Auth.Domain.DTOs;
using Auth.Domain.Entities;
using Auth.Infrastructure.Context;
using AutoMapper;
using BaseCrud.Abstractions.Entities;
using BaseCrud.EntityFrameworkCore;
using BaseCrud.Errors;
using BaseCrud.ServiceResults;
using General.DTOs;
using General.Enums;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Services;

public class UserService(
    AuthContext authContext,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IHttpClientFactory httpClientFactory,
    IMapper mapper
    ) : BaseCrudService<ApplicationUser,UserDto,UserDto,Guid,Guid>(authContext,mapper), IUserService
{


    public async Task<ServiceResult<LanguageDto>> ChangeLanguage(string lang, IUserProfile<Guid> userProfile)
    {
        var userId = userProfile.Id;

        var user = await userManager.FindByIdAsync(userId.ToString());

        user.Language = lang switch
        {
            "uz" => Languages.UZBEK,
            "ru" => Languages.RUSSIAN,
            "kr" => Languages.KARAKALPAK,
            _ => Languages.ENGLISH
        };

        await userManager.UpdateAsync(user);

        var langDto = new LanguageDto
        {
            Language = user.Language.ToString()
        };

        return ServiceResult.Ok(langDto);
    }

    public async Task<ServiceResult<bool>> Refresh()
    {

        return ServiceResult.Ok(false);
    }

    public async Task<ServiceResult<LanguageDto>> UserLanguage(IUserProfile<Guid> userProfile)
    {
        var user =await userManager.FindByIdAsync(userProfile.Id.ToString());

        if (user == null)
            return ServiceResult.NotFound(new ServiceError("User not found", "UserService"));

        return ServiceResult.Ok(new LanguageDto { Language = user.Language.ToString() });
    }


    public async Task<ServiceResult<bool>> Init()
    {
        var defaultPassword = "Qwerty!23";

        var roles = new List<ApplicationRole>
        {
            new ApplicationRole {Name = "admin"},
            new ApplicationRole {Name = "teacher"},
            new ApplicationRole {Name = "student"},
            new ApplicationRole{Name = "user"}
        };


        foreach (var role in roles)
        {
            if (await roleManager.RoleExistsAsync(role.Name))
                continue;
             await roleManager.CreateAsync(role);
          
        }

        var users = new List<ApplicationUser>
        {
            new ApplicationUser{
                UserName = "admin",
                FirstName = "admin",
                LastName = "admin",
                MainRole="admin",
                Active=true,
                AuthDate=DateTime.Now.ToString(),
                Language=Languages.ENGLISH,
                Email = "",
            },
            new ApplicationUser
        {
            UserName = "teacher",
            FirstName = "teacher",
            LastName = "teacher",
            MainRole= "teacher",
            Active=true,
                AuthDate=DateTime.Now.ToString(),
                Language=Languages.ENGLISH,
                Email = "",
        },
             new ApplicationUser
        {
            UserName = "student",
            FirstName = "student",
            LastName = "student",
            MainRole= "student",
            Active=true,
                AuthDate=DateTime.Now.ToString(),
                Language=Languages.ENGLISH,
                Email = "",
        },
             new ApplicationUser
        {
            UserName = "user",
            FirstName = "user",
            LastName = "user",
            MainRole= "user",
            Active=true,
                AuthDate=DateTime.Now.ToString(),
                Language=Languages.ENGLISH,
                Email = "",
        }
        };

        foreach(var user in users)
        {
            if(await userManager.FindByNameAsync(user.UserName) != null)
                continue;
            await userManager.CreateAsync(user,defaultPassword);
            await userManager.AddToRoleAsync(user, user.UserName);
            if(user.UserName!= "user")
                await userManager.AddToRoleAsync(user, "user");
        }
        return ServiceResult.Ok(true);
    }
}
