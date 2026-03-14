using Microsoft.AspNetCore.Identity;
using Shared.Custom;
using Shared.Custom.Interfaces;

namespace Domain.Users;

public class User : IdentityUser<Guid>, IMultiTenant
{
    public Guid TenantId { get; set; }
}