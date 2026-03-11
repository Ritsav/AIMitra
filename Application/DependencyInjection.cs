using Application.Languages;
using Application.Products;
using Application.Users;
using Contracts.Languages;
using Contracts.Products;
using Contracts.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        #region Services
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<ILanguageManager, LanguageManager>();
        
        #endregion
        
        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));

        return services;
    }
}