using BaseCrud.Abstractions.Entities;

namespace Test.Domain.Entities;

public class Answer:EntityBase
{

    public string? Description { get; set; }
    public bool IsCorrect { get; set; }

    public Guid ?FileInfosId { get; set; }
    public FileInfos? FileInfos { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}
