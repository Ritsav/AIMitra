using System.ComponentModel.DataAnnotations;
using Shared.Skus;

namespace Contracts.Skus;

public abstract class BaseSkuDto
{
    [Required]
    public Guid ProductId { get; set; }
    
    [Range(SkuConst.MinPriceRange, SkuConst.MaxPriceRange)]
    public decimal Price { get; set; }
    
    public uint Quantity { get; set; }

    public ICollection<SkuTranslationDto> Translations { get; set; } =
        new List<SkuTranslationDto>();
}