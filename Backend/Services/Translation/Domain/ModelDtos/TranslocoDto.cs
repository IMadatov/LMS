using BaseCrud.Entities;
using Domain.Models;

namespace Domain.ModelDtos;

public class TranslocoDto:IDataTransferObject<Transloco>
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? ValueUZ { get; set; }
    public string? ValueRU { get; set; }
    public string? ValueEN { get; set; }
    public string? ValueKR { get; set; }
}
