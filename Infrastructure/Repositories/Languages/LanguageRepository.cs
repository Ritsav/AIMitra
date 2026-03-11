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
        var languageMissing = await dbContext.Languages
            .AnyAsync(x => !languageIds.Contains(x.Id));

        if (languageMissing)
            throw new Exception("One or more languages do not exist.");
    }
}