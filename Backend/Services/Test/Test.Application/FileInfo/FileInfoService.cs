using LearnSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Test.Domain.Entities;
using Test.Infrastucture;

namespace Test.Application.FileInfo;

public class FileInfoService(IConfiguration configuration, TestsContext context) : IFileInfoService
{



    public async Task<string?> FileByIdInBase64(Guid id)
    {
        var fileInfos = context.fileInfos.FirstOrDefault(x => x.Id == id);

        var rootPath = RootPath();
        return await FileInfoServiceHelper.GetStringBase64FileAsync(rootPath, fileInfos);
    }

    public async Task<FileContentResult> GetFile(Guid id)
    {
        var fileInfos = context.fileInfos.FirstOrDefault(x => x.Id == id);

        var rootPath = RootPath();

        return await FileInfoServiceHelper.GetFileAsync(rootPath, fileInfos);

    }

    public async Task<Guid> UploadFileAsync(IFormFile file)
    {
        var helper = new FileInfoServiceHelper();
        var rootPath = RootPath();

        helper.CreateFolder(rootPath);
        await helper.SaveFileAsync(file);

        var item = new FileInfos
        {
            ContentPath = helper.FilePath ?? throw new ArgumentNullException(nameof(helper.FilePath)),
            FileName = file.FileName,
            ContentType = file.ContentType,
            ContentLength = file.Length,

        };

        context.fileInfos.Add(item);

        context.SaveChanges();

        return item.Id;
    }

    private string RootPath() => configuration["Files:Path"] ?? throw new ArgumentNullException(nameof(RootPath));

}
