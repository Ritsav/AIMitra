using Contracts.Languages;
using Domain.Languages;

namespace Application.Languages;

public class LanguageManager(
    ILanguageRepository languageRepository) : ILanguageManager
{
    public async Task ValidateLanguagesAsync(ICollection<Guid> languageIds)
    {
        await languageRepository.ValidateLanguagesAsync(languageIds);
    }
}