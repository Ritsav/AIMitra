using AutoMapper;
using Contracts.Languages;
using Domain.Languages;

namespace Application.Languages;

public class LanguageService(
    ILanguageRepository languageRepository,
    IMapper mapper) : ILanguageService
{
    public async Task<List<LanguageDto>> GetListAsync()
    {
        var languages = await languageRepository.GetListAsync();
        return mapper.Map<List<Language>, List<LanguageDto>>(languages);
    }
}