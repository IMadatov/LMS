using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Test.Domain.Entities;

namespace Test.Application.FileInfo;

public class FileInfoServiceHelper
{
    public string FolderPathFull { get; set; }
    public string FolderPath { get; set; }

    public string FilePathFull { get; set; }
    public string FilePath { get; set; }

    public void CreateFolder(string rootPath)
    {
        FolderPath = $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";

        FolderPathFull = Path.Combine(rootPath, FolderPath);

        if (!Directory.Exists(FolderPathFull))
        {
            Directory.CreateDirectory(FolderPathFull);
        }
    }

    public async Task SaveFileAsync(IFormFile file)
    {
        var guidFileName = Guid.NewGuid();

        var fileExtension = file.FileName[file.FileName.LastIndexOf(".", StringComparison.Ordinal)..];

        FilePath = $"{FolderPath}/{guidFileName}{fileExtension}";

        FilePathFull = $"{FolderPathFull}/{guidFileName}{fileExtension}";

        await using Stream fileStream = new FileStream(FilePathFull, FileMode.Create);

        await file.CopyToAsync(fileStream);
    }


    public static async Task<string> GetStringBase64FileAsync(string rootPath, FileInfos? file)
    {
        if (file == null)
        {
            return null;
        }

        var fileFullPath = Path.Combine(rootPath, file.ContentPath);

        if (!File.Exists(fileFullPath))
        {
            return null;
        }
        var bytes = await File.ReadAllBytesAsync(fileFullPath);

        return $"data:{file.ContentPath};base64:"+Convert.ToBase64String(bytes);
    }


    public static async Task<FileContentResult> GetFileAsync(string rootPath,FileInfos? file)
    {
        if (file == null)
            return null;

        var fileFullPath = Path.Combine(rootPath, file.ContentPath);

        if (!File.Exists(fileFullPath))
            return null;

        var bytes = await File.ReadAllBytesAsync(fileFullPath);

        

        return new FileContentResult(bytes, file.ContentType) {
            FileDownloadName = file.FileName
        };
    }

}
