using Contracts.Skus;

namespace Contracts.Products;

public class CreateUpdateProductDto : BaseProductDto
{
    public ICollection<CreateUpdateSkuDto> Skus { get; set; } =
        new List<CreateUpdateSkuDto>();
}