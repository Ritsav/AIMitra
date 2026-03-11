namespace Contracts.Languages;

public interface ILanguageManager
{
    Task ValidateLanguagesAsync(ICollection<Guid> languageIds);
}