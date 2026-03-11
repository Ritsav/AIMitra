using Application.Users;
using Contracts.AIServices;
using Contracts.Users;
using Domain;
using Domain.Languages;
using Domain.Products;
using Domain.Users;
using Infrastructure.AiServices;
using Infrastructure.Data;
using Infrastructure.Repositories.Languages;
using Infrastructure.Repositories.Products;
using Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Shared.Custom;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration
                .GetConnectionString("DefaultConnection"));
        });
        
        services.AddKernel()
            .AddGoogleAIGeminiChatCompletion(
                modelId: "gemini-2.5-flash",
                apiKey: configuration
                    .GetSection("ApiKeys")
                    .GetSection("Gemini")
                    .Value!);
        
        services.AddScoped<IAiService, SemanticKernalAiService>();
        
        #region Identity Configurations

        services.Configure<IdentityOptions>(options =>
        {
            // Lockout options
            options.Lockout.DefaultLockoutTimeSpan = AppConst.DefaultLockoutTimeSpan;
            options.Lockout.MaxFailedAccessAttempts = AppConst.MaxFailedAccessAttempts;
    
            // User options
            options.User.RequireUniqueEmail = AppConst.RequireUniqueEmail;
            options.User.AllowedUserNameCharacters = AppConst.AllowedUserNameCharacters;
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = AppConst.CookieExpirationTimeSpan;
            options.SlidingExpiration = true;
        });
        
        #endregion
        
        #region Repositories
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        
        #endregion
        
        return services;
    }
}