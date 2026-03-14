using Infrastructure.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class DesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly TenantProvider _tenantProvider;
    
    public DesignTimeDbContextFactory(TenantProvider tenantProvider)
    {
        _tenantProvider = tenantProvider;
    }
    
    public DesignTimeDbContextFactory()
    {
    }
    
    public AppDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../WebAPI");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration
            .GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options, _tenantProvider);
    }
}