using Contracts.AIServices;
using Microsoft.SemanticKernel;

namespace Infrastructure.AiServices;

public class SemanticKernalAiService(Kernel kernal) : IAiService
{
    public async Task<string> GenerateAsync(string prompt)
    {
        var result = await kernal.InvokePromptAsync(prompt);
        return result.ToString();
    }
}