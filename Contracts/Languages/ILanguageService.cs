namespace Contracts.Languages;

public interface ILanguageService
{
    public Task<List<LanguageDto>> GetListAsync();
}