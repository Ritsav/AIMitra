using Contracts.AIServices;
using Contracts.Users;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Users;

[ApiController]
[Route("/api/[controller]")]
public class UserController(
    IAiService aiService,
    IUserService userService,
    SignInManager<User> signInManager) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<string> MessageAiAsync(string msg)
    {
        return await aiService.GenerateAsync(msg);
    }
    
    [HttpGet]
    [Authorize]
    [Route("info")]
    public async Task<ActionResult<UserInfoDto>> GetUserInfoAsync(
        [FromQuery] UserInfoRequestDto request)
    {
        try
        {
            var user = await userService.GetUserInfoAsync(request);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> LogoutAsync([FromBody] object empty)
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
        return Unauthorized();
    }
}