

namespace GTranslate.Services.IServices;

public interface IGTranslateService
{
    Task<string> GetTranslation(string Text, string SourceLanguage);

}
