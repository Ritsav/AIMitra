using Infrastructure.AiServices;
using Microsoft.SemanticKernel;

namespace AIMitra.Tests.AiServices;

public class IsPromptGoingThrough
{
    [Fact]
    public async Task IsPromptGoingThroughTest()
    {
        var kernal = new Kernel();
        
        var aiService = new SemanticKernalAiService(kernal);
        var response = await aiService
            .GenerateAsync("What is the capital of France?");
        
        Assert.NotNull(response);
    }
}