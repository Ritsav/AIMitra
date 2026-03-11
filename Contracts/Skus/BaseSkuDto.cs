using System.ComponentModel.DataAnnotations;
using Shared.Custom;
using Shared.Skus;

namespace Contracts.Skus;

public abstract class BaseSkuDto
{
    [Range(SkuConst.MinPriceRange, SkuConst.MaxPriceRange)]
    public decimal Price { get; set; }
    
    public uint Quantity { get; set; }

    public ICollection<SkuTranslationDto> Translations { get; set; } =
        new List<SkuTranslationDto>();
}