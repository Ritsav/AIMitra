namespace Contracts.Products;

public abstract class BaseProductDto
{
    public ICollection<ProductTranslationDto> Translations { get; set; } =
        new List<ProductTranslationDto>();
}