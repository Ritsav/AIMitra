using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.MultiTenancy;

public sealed class TenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid GetTenantId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        
        if (httpContext == null)
            throw new InvalidOperationException("No HTTP context available.");
        
        if(httpContext?.User?.Identity?.IsAuthenticated != true)
            throw new InvalidOperationException("User is not authenticated.");
            
        var userIdClaim = httpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (!Guid.TryParse(userIdClaim, out var tenantId))
            throw new InvalidOperationException("Unable to resolve tenant.");
        
        return tenantId;
    }
}