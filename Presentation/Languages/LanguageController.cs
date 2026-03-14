using Contracts.Languages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Languages;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class LanguageController(ILanguageService languageService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LanguageDto>>> GetListAsync()
    {
        var languages = await languageService.GetListAsync();
        return Ok(languages);
    }
}