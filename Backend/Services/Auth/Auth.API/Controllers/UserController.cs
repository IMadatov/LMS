using Auth.Application.Services;
using Auth.Domain.DTOs;
using BaseCrud.Abstractions.Entities;
using BaseCrud.Entities;
using BaseCrud.PrimeNg;
using General.DTOs;
using General.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.API.Controllers;

[Authorize]
public class UserController(IUserService userService):BaseController
{
    [HttpGet]
    public async Task<ActionResult<UserDto?>> Me()
    =>await FromServiceResult(userService.Me(UserProfile));

    [HttpPost]
    public async Task<ActionResult<QueryResult<UserDto>?>> GetAll(PrimeTableMetaData dataTableMetaData)
    {
        return await FromServiceResult(userService.GetAllAsync(dataTableMetaData, UserProfile));
    }

    [HttpGet]
    public async Task<ActionResult<LanguageDto?>> MyLanguage() =>
        await FromServiceResult(userService.UserLanguage(UserProfile));

    [HttpPost]
    public async Task<ActionResult<LanguageDto?>> ChangeMyLanguage(Languages language) =>
        await FromServiceResult(userService.ChangeLanguage(language, UserProfile));

    
}
