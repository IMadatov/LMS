using BaseCrud.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Domain.Entities;

public class UserAnswer:EntityBase<int>
{

    public Guid UserId { get; set; }

    [ForeignKey("TestModel")]
    public int TestModelId { get; set; }
    public TestModel TestModel { get; set; }
 
    [ForeignKey("Question")]
    public int QuestionId { get; set; }  
    public Question Question { get; set; }
    
    [ForeignKey("Answer")]
    public int AnswerId { get; set; }
    public Answer Answer { get; set; }
}
