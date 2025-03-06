using BaseCrud.Abstractions.Entities;

namespace Test.Domain.Entities;

public class FileInfos:EntityBase
{
    public new Guid Id { get; set; }

    public string ContentPath { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public required string ContentType { get; set; }

    public long ContentLength { get; set; }

    public int? GroupId { get; set; }
}
