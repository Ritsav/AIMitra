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
}