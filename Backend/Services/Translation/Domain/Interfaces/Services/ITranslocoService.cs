using BaseCrud.Abstractions.Entities;
using BaseCrud.Abstractions.Services;
using BaseCrud.ServiceResults;
using Domain.ModelDtos;
using Domain.Models;

namespace Domain.Interfaces.Services;

public interface ITranslocoService:ICrudService<Transloco,TranslocoDto,TranslocoDto,Guid>
{

    Task<List<KeyValuePair<string, string>>> GetTranslationsAsync(string lang);

    Task<string> CurrentLanguage(string lang);
    Task<bool> DeleteAsync(int id, IUserProfile<Guid>? userProfile);
    Task<ServiceResult<bool>> InsertAutoTranslation(List<TranslocoDto> translocoDtos);
}
