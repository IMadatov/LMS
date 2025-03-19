using Auth.Domain.Entities;
using Auth.Infrastructure.Context;
using AutoMapper;
using BaseCrud.Abstractions.Entities;
using BaseCrud.Errors;
using BaseCrud.ServiceResults;
using General.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Services
{
    public class UserService(
        AuthContext authContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IHttpClientFactory httpClientFactory,
        IMapper mapper
        ) : IUserService
    {


        public Task<ServiceResult<LanguageDto>> ChangeLanguage(string lang, IUserProfile<Guid> userProfile)
        {

            throw new NotImplementedException();
        }

        public async Task<ServiceResult<UserDto>> Me(IUserProfile<Guid> userProfile)
        {
            var user = await userManager.FindByIdAsync(userProfile.Id.ToString());

            if (user == null)
                return ServiceResult.NotFound(new ServiceError("User not found", "UserService"));

            var userDto = mapper.Map<UserDto>(user);

            return ServiceResult.Ok(userDto);
        }

        public async Task<ServiceResult<bool>> Refresh()
        {

            return ServiceResult.Ok(false);
        }

        public Task<ServiceResult<LanguageDto>> UserLanguage(IUserProfile<Guid> userProfile)
        {
            throw new NotImplementedException();
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
                    Email = "",
                },
                new ApplicationUser
            {
                UserName = "teacher",
                FirstName = "teacher",
                LastName = "teacher",
                Email = "",
            },
                 new ApplicationUser
            {
                UserName = "student",
                FirstName = "student",
                LastName = "student",
                Email = "",
            },
                 new ApplicationUser
            {
                UserName = "user",
                FirstName = "user",
                LastName = "user",
                Email = "",
            }
            };

            foreach(var user in users)
            {
                if(await userManager.FindByNameAsync(user.UserName) != null)
                    continue;
                await userManager.CreateAsync(user,defaultPassword);
                await userManager.AddToRoleAsync(user, user.UserName);

            }

            return ServiceResult.Ok(true);
        }
    }
}
