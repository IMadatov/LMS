

using GoogleTranslateFreeApi;
using GTranslate.Services.IServices;

namespace GTranslate.Services;

public class GTranslateService : IGTranslateService
{
    GoogleTranslator translator=new();
    public GTranslateService()
    {
    }
    public async Task<string> GetTranslation(string Text, string SourceLanguage)
    {

        var result = await translator.TranslateAsync(Text, Language.Uzbek, Language.English);
        return result.MergedTranslation;
    }
}