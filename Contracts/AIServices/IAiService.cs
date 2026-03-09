namespace Contracts.AIServices;

public interface IAiService
{
    Task<string> GenerateAsync(string prompt);
}