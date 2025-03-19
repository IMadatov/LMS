
using BaseCrud.Abstractions.Entities;
using BaseCrud.PrimeNg;
using Domain.Interfaces.Services;
using Domain.ModelDtos;
using GTranslate.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Translation.API.Controllers;

[Authorize]
public class TranslocoController(ITranslocoService translocoService,IGTranslateService gTranslateService) : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<QueryResult<TranslocoDto>?>> GetTranslations(PrimeTableMetaData data)=>
        await FromServiceResult( translocoService.GetAllAsync(data,null));


    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<TranslocoDto?>> InsertOrUpdateWord(TranslocoDto translocoDto) =>
        await FromServiceResult(
            translocoDto.Id==0
            ?translocoService.InsertAsync(translocoDto,UserProfile):translocoService.UpdateAsync(translocoDto,UserProfile));

   

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<string>> CurrentLanguage(string lang) =>
        await translocoService.CurrentLanguage(lang);


    [HttpDelete]
    [Authorize(Roles="admin")]
    public async Task<ActionResult<bool>> DeleteWord(int id) =>
        await   translocoService.DeleteAsync(id,UserProfile);

    [HttpPost]
    public async Task<ActionResult<string>> Translate(string TextUz)=>
        await gTranslateService.GetTranslation(TextUz,"");

}
