using BaseCrud.Abstractions.Entities;

namespace Test.Domain.Entities;

public class TestModel:EntityBase
{
    public string TestName { get; set; } = string.Empty;
    public string Description { get; set; }
    
    public Guid? FileInfosId { get; set; }
    public FileInfos? FileInfos { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int CountQuestion { get; set; }
    public ICollection<Question> Questions { get; set; } = new List<Question>();

}
