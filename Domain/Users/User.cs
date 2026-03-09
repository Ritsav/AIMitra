using Microsoft.AspNetCore.Identity;
using Shared.Custom;
using Shared.Custom.Interfaces;

namespace Domain.Users;

public class User : IdentityUser<Guid>, IMultiTenant
{
    // TODO: Set the tenantId as the userId during registration, and use that for multi-tenancy.
    // This way, we can easily associate users with their respective tenants.
    public Guid TenantId { get; set; }
}