using Shared.Custom.Interfaces;

namespace Contracts.Skus;

public class SkuDto : BaseSkuDto, IGuidEntityDto
{
    public Guid Id { get; set; }
}
