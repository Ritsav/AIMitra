using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity;

public class AppUserManager(
    IUserStore<User> store,
    IOptions<IdentityOptions> optionsAccessor,
    IPasswordHasher<User> passwordHasher,
    IEnumerable<IUserValidator<User>> userValidators,
    IEnumerable<IPasswordValidator<User>> passwordValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    IServiceProvider services,
    ILogger<UserManager<User>> logger)
    : UserManager<User>(store, optionsAccessor, passwordHasher,
        userValidators, passwordValidators, keyNormalizer, errors, services, logger)
{
    public override async Task<IdentityResult> CreateAsync(User user, string password)
    {
        user.Id = Guid.NewGuid();
        user.TenantId = user.Id;
        return await base.CreateAsync(user, password);
    }
}
