namespace Domain.Languages;

public interface ILanguageRepository
{
    Task<List<Language>> GetListAsync();
}