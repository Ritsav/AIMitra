using Shared.Custom.Interfaces;

namespace Contracts.Products;

public class ProductDto : BaseProductDto, IGuidEntityDto
{
    public Guid Id { get; set; }
}