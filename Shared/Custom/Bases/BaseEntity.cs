using Shared.Custom.Interfaces;

namespace Shared.Custom.Bases;

public abstract class BaseEntity : IMultiTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
}