using AutoMapper;
using BaseCrud.Abstractions.Entities;
using BaseCrud.EntityFrameworkCore;
using BaseCrud.ServiceResults;
using Domain.Interfaces.Services;
using Domain.ModelDtos;
using Domain.Models;
using Infrastructure.DbContextOptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Application.Services;

public class TranslocoService(
    TranslationDbContext dbContext, 
    IMapper mapper) 
    : BaseCrudService<Transloco, TranslocoDto, TranslocoDto,Guid>(dbContext, mapper),
    ITranslocoService
{
    public async Task<string> CurrentLanguage(string lang)
    {
        var json = new JObject();

        var dataDb =await dbContext.Translations.ToListAsync();

        dataDb.ForEach(dd =>
        {
            switch (lang)
            {
                case "uz":
                    json[dd.Code] = dd.ValueUZ;
                    break;
                case "kr":
                    json[dd.Code] = dd.ValueKR;
                    break;
                case "ru":
                    json[dd.Code] = dd.ValueRU;
                    break;

                default:
                    json[dd.Code] = dd.ValueEN;
                    break;
            }
        });

        return json.ToString();

    }

    public  async Task<bool> DeleteAsync(int id, IUserProfile<Guid>? userProfile)
    {

        var word=await dbContext.Translations.FirstOrDefaultAsync(t => t.Id == id);

        if(word is not null)
        {
            dbContext.Remove(word);
            await dbContext.SaveChangesAsync();
        }

        return true;
    }

    public async Task<List<KeyValuePair<string, string>>> GetTranslationsAsync(string lang)
    {
        if(lang is null)
        {
            return [];
        }


        var translations = dbContext.Translations.AsQueryable();
        List< KeyValuePair<string, string> >CurrentLanguage = new();
        switch (lang)
        {
            case "RU_ru":
                CurrentLanguage= translations.Select(t =>new KeyValuePair<string,string>(t.Code,t.ValueRU)).ToList();
                break;
                    
            case "UZ_uz":

                CurrentLanguage = translations.Select(t => new KeyValuePair<string, string>(t.Code, t.ValueUZ)).ToList();
                break;
            case "KR_kr":

                CurrentLanguage = translations.Select(t => new KeyValuePair<string, string>(t.Code, t.ValueKR)).ToList();
                break;
            case "EN_en":

                CurrentLanguage = translations.Select(t => new KeyValuePair<string, string>(t.Code, t.ValueEN)).ToList();
                break;
        }



        return CurrentLanguage;
    }

    public async Task<ServiceResult<bool>> InsertAutoTranslation(List<TranslocoDto> translocoDtos)
    {
        var translocos=Mapper.Map<List<Transloco>>(translocoDtos);

        DbContext.AddRange(translocos);

        await DbContext.SaveChangesAsync();

        return ServiceResult.Ok(true);
    }
}
