namespace Shared.Custom.Interfaces;

public interface IMultiTenant
{
    public Guid TenantId { get; set; }
}