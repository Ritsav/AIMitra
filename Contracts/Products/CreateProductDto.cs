using Contracts.Skus;

namespace Contracts.Products;

public class CreateProductDto : BaseProductDto
{
    public ICollection<CreateSkuDto> Skus { get; set; } =
        new List<CreateSkuDto>();
}