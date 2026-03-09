namespace Shared.Custom;

public static class AppConst
{
    // Db Consts
    public const string DbTablePrefix = "App";
    
    // Language Consts
    public const int MaxLanguageCodeLength = 4;
    public const int MaxLanguageNameLength = 100;
    
    // Translation Consts
    public const int MaxTranslationNameLength = 100;
    public const int MaxTranslationDescriptionLength = 500;
    
    // User Consts
    public const string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.";
    public const bool RequireUniqueEmail = true;
    public const int MaxFailedAccessAttempts = 7;
    public static readonly TimeSpan DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    
    
    // Cookie Consts
    public static readonly TimeSpan CookieExpirationTimeSpan = TimeSpan.FromDays(7);
    
    // Decimal Precision
    public const int DecimalPrecisionDigits = 7;
    public const int DecimalPrecisionPoints = 2;
}