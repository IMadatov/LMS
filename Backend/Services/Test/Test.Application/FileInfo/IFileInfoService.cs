using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Application.FileInfo;

public interface IFileInfoService
{
    Task<Guid> UploadFileAsync(IFormFile file);

    Task<string?> FileByIdInBase64(Guid id);

    Task<FileContentResult> GetFile(Guid id);

}
