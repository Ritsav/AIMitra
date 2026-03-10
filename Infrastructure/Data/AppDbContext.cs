using Domain.Languages;
using Domain.Products;
using Domain.SKUs;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Custom;
using Shared.Languages;

namespace Infrastructure.Data;

public class AppDbContext 
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    #region DbSets
    
    // Languages (shared across all tenants)
    public DbSet<Language> Languages { get; set; }

    // Products
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductTranslation> ProductTranslations { get; set; }
    
    // SKUs
    public DbSet<Sku> Skus { get; set; }
    public DbSet<SkuTranslation> SkuTranslations { get; set; }

    #endregion
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        IdentityModelBuilders.BuildIdentityModel(builder);

        builder.Entity<Language>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(Languages));

            b.HasData(
                new Language { Id = LanguageConst.EnglishId, Code = "en", Name = "English" },
                new Language { Id = LanguageConst.NepaliId, Code = "ne", Name = "Nepali" }
            );
        });
        
        #region Products

        builder.Entity<Product>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(Products));
            
            b.HasMany(x => x.Translations)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            
            b.HasMany(x => x.Skus)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

            b.Navigation(x => x.Skus)
                .AutoInclude();
            
            b.Navigation(x => x.Translations)
                .AutoInclude();
        });

        builder.Entity<ProductTranslation>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(ProductTranslations));
        });
        
        #endregion
        
        #region Skus

        builder.Entity<Sku>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(Skus));
            
            b.HasMany(x => x.Translations)
                .WithOne(x => x.Sku)
                .HasForeignKey(x => x.SkuId);
            
            b.Navigation(x => x.Translations)
                .AutoInclude();
        });

        builder.Entity<SkuTranslation>(b =>
            b.ToTable(AppConst.DbTablePrefix + nameof(SkuTranslations)));
        
        #endregion
    }
}