using Microsoft.AspNetCore.Mvc;
using Test.Application.FileInfo;

namespace Test.API.Controllers;


public class TestController(IFileInfoService _fileInfoService) :BaseController
{
    [HttpPost()]
    public async Task<ActionResult> UploadFile(IFormFile file)
    {
        if(file is null)
            return BadRequest();
        //if (!Request.HasFormContentType)
        //return BadRequest();

        //var form = Request.Form;

        return Ok(
                    await _fileInfoService.UploadFileAsync(file)
                    );
    }

    [HttpGet]
    public async Task<ActionResult> GetFile(Guid id)
    {

        var file = await _fileInfoService.GetFile(id);
        if(file is null)
            return NotFound();

        return File(file.FileContents, file.ContentType, file.FileDownloadName);
    }

    [HttpGet]
    public async Task<ActionResult> GetFileByIdInBase64(Guid id)
    {
        return Ok(
            await _fileInfoService.FileByIdInBase64(id)
        );
    }
}
