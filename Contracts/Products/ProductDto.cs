using Contracts.Skus;
using Shared.Custom.Interfaces;

namespace Contracts.Products;

public class ProductDto : BaseProductDto, IGuidEntityDto
{
    public Guid Id { get; set; }
    public uint TotalQuantity { get; set; }
    
    public ICollection<SkuDto> Skus { get; set; } =
        new List<SkuDto>();
}