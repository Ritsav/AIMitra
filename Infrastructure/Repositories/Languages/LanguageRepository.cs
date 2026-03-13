using Domain.Languages;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Languages;

public class LanguageRepository(AppDbContext dbContext) : ILanguageRepository
{
    public async Task<List<Language>> GetListAsync()
    {
        return await dbContext.Languages.ToListAsync();
    }

    public async Task ValidateLanguagesAsync(ICollection<Guid> languageIds)
    {
        var filteredLanguageIds = languageIds
            .Distinct()
            .ToList();
        
        var languages = await dbContext.Languages
            .Where(x => filteredLanguageIds.Contains(x.Id))
            .ToListAsync();

        if (filteredLanguageIds.Count != languages.Count)
            throw new Exception("One or more languages do not exist.");
    }
}