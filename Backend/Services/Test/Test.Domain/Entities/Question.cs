using BaseCrud.Abstractions.Entities;

namespace Test.Domain.Entities;

public class Question:EntityBase
{
    public string? Description { get; set; }
    public uint Point { get; set; }
    public Guid? FileInfosId { get; set; }
    public FileInfos? FileInfos { get; set; }
    public int TestId { get; set; } 
    public TestModel Test { get; set; }
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
