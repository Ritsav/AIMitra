using Domain.Customers;
using Domain.Languages;
using Domain.Products;
using Domain.SKUs;
using Domain.Users;
using Infrastructure.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Custom;
using Shared.Custom.Interfaces;
using Shared.Languages;

namespace Infrastructure.Data;

public class AppDbContext
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly TenantProvider _tenantProvider;
    
    public AppDbContext(DbContextOptions<AppDbContext> options,
        TenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Guid? tenantId = null;

        foreach (var entry in ChangeTracker.Entries<IMultiTenant>())
        {
            if (entry.State != EntityState.Added || entry.Entity.TenantId != Guid.Empty)
                continue;

            tenantId ??= _tenantProvider.GetTenantId();
            entry.Entity.TenantId = tenantId.Value;
        }

        return await base.SaveChangesAsync(cancellationToken);
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

    // Customers
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerTranslation> CustomerTranslations { get; set; }
    
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
            b.HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());
            
            b.HasMany(x => x.Translations)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            
            b.HasMany(x => x.Skus)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.Skus)
                .AutoInclude();
            
            b.Navigation(x => x.Translations)
                .AutoInclude();
        });

        builder.Entity<ProductTranslation>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(ProductTranslations));
            b.HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());
        });
        
        #endregion
        
        #region Skus

        builder.Entity<Sku>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(Skus));
            b.HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());
            
            b.HasMany(x => x.Translations)
                .WithOne(x => x.Sku)
                .HasForeignKey(x => x.SkuId)
                .OnDelete(DeleteBehavior.Cascade);
            
            b.Navigation(x => x.Translations)
                .AutoInclude();
        });

        builder.Entity<SkuTranslation>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(SkuTranslations));
            b.HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());
        });
        
        #endregion
        
        #region Customers
        
        builder.Entity<Customer>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(Customers));
            b.HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());
            
            b.HasMany(x => x.Translations)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            b.Navigation(x => x.Translations)
                .AutoInclude();
        });

        builder.Entity<CustomerTranslation>(b =>
        {
            b.ToTable(AppConst.DbTablePrefix + nameof(CustomerTranslations));
            b.HasQueryFilter(x => x.TenantId == _tenantProvider.GetTenantId());
        });

        #endregion
    }
}
