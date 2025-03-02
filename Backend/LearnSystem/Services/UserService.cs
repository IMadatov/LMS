using AutoMapper;
using BaseCrud.Abstractions.Entities;
using BaseCrud.EntityFrameworkCore;
using Domain.Models;
using LearnSystem.DbContext;
using LearnSystem.Models;
using LearnSystem.Models.ModelsDTO;
using LearnSystem.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceStatusResult;

namespace LearnSystem.Services;


public class UserService(
    ApplicationDbContext _context,
    IHttpContextAccessor _httpContextAccessor,
    UserManager<User> userManager,
    IMapper autoMapper,
    RoleManager<ApplicationRole> roleManager
) : IUserService
{
    public async Task<ServiceResultBase<UserDto>> Me(Guid userId)
    {

        var user = await userManager.FindByIdAsync(userId.ToString());

        user = await _context.Users.Include(x => x.StatusUser).FirstOrDefaultAsync(x => x.Id == user.Id);

        if (user == null)
        {
            return new NotFoundServiceResult<UserDto>();
        }

        var roles = await userManager.GetRolesAsync(user);
        var status = user.StatusUser;

        var userDto = autoMapper.Map<UserDto>(user);
        userDto.Roles = roles;
        userDto.StatusUser = autoMapper.Map<StatusUserDto>(status);

        return new OkServiceResult<UserDto>("profile", userDto);
    }

    public async Task<ServiceResultBase<LanguageDto>> UserLanguage(IUserProfile<Guid> userProfile)
    {
        var user = await userManager.FindByIdAsync(userProfile.Id.ToString());

        string language= "en";

        switch (user.Language)
        {
            case Languages.UZBEK:
                language = "uz";
                break;
            case Languages.RUSSIAN:
                language = "ru";
                break;
            case Languages.KARAKALPAK:
                language = "kr";
                break;

            default:
                language = "en";
                break;
        }

        return new OkServiceResult<LanguageDto>(new LanguageDto { Language = language });
    }


    

    public async Task<ServiceResultBase<bool>> Refresh()
    {

        return new OkServiceResult<bool>(true);
    }

    public async Task<ServiceResultBase<LanguageDto>> ChangeLanguage(string lang,IUserProfile<Guid> userProfile)
    {
        Languages language;

        switch (lang)
        {
            case "uz":
                language = Languages.UZBEK;
                break;

            case "ru":
                language = Languages.RUSSIAN;
                break;

            case "kr":
                language = Languages.KARAKALPAK;
                break;

            default:
                language = Languages.ENGLISH;
                break;
        }

        var user =await _context.Users.FirstOrDefaultAsync(u=>u.Id==userProfile.Id);

        user.Language = language;


        _context.Entry(user).State = EntityState.Modified;


        _context.SaveChanges();

        return new OkServiceResult<LanguageDto>(new LanguageDto { Language = lang });
    }
}

