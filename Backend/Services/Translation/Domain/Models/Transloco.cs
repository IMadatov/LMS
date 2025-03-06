using BaseCrud.Abstractions.Entities;
using BaseCrud.Abstractions.Expressions;
using System.Linq.Expressions;

namespace Domain.Models;

public class Transloco : EntityBase
{

    public required string Code { get; set; }
    public required string ValueUZ { get; set; }
    public required string ValueRU { get; set; }
    public required string ValueEN { get; set; }
    public required string ValueKR { get; set; }
}


public class TranslocoExpressions : IGlobalFilterExpression<Transloco>
{
    public Expression<Func<Transloco, bool>> GlobalSearchExpression(string strSearch)
        => t => t.ValueEN.Contains(strSearch) || t.ValueRU.Contains(strSearch) || t.ValueUZ.Contains(strSearch)||t.ValueKR.Contains(strSearch) || t.Code.Contains(strSearch);
}
