using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Custom;

namespace Infrastructure.Data;

public class IdentityModelBuilders : AppDbContext
{
    public IdentityModelBuilders(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public static void BuildIdentityModel(ModelBuilder builder)
    {
        builder.Entity<User>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(Users));
        });

        builder.Entity<IdentityRoleClaim<Guid>>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(RoleClaims));
        });

        builder.Entity<IdentityRole<Guid>>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(Roles));
        });

        builder.Entity<IdentityUserClaim<Guid>>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(UserClaims));
        });

        builder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(UserLogins));
        });

        builder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(UserRoles));
        });

        builder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(UserTokens));
        });
    }
}