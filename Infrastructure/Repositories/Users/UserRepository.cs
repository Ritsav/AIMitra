using Domain.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

public class UserRepository(
    AppDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetUserByIdentifierAsync(string identifier)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => 
            x.NormalizedEmail == identifier.ToUpperInvariant() ||
            x.NormalizedUserName == identifier.ToUpperInvariant());
        
        return user;
    }
}