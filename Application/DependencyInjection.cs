using Application.Custom;
using Application.Languages;
using Application.Products;
using Application.Users;
using Contracts.Languages;
using Contracts.Products;
using Contracts.Users;
using Microsoft.Extensions.DependencyInjection;
using Shared.Custom.Interfaces;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        #region Services & Managers
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ILanguageService, LanguageService>();
        
        // Managers
        services.AddScoped<ILanguageManager, LanguageManager>();
        
        #endregion
        
        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));

        return services;
    }
}