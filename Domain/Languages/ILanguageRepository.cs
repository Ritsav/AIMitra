namespace Domain.Languages;

public interface ILanguageRepository
{
    Task<List<Language>> GetListAsync();
    
    Task ValidateLanguagesAsync(ICollection<Guid> languageIds);
}