using System.ComponentModel.DataAnnotations;
using Domain.Languages;
using Shared.Custom;
using Shared.Custom.Bases;
using Shared.Languages;

namespace Domain.SKUs;

public class SkuTranslation : BaseEntity, IHasTranslation
{
    public Guid SkuId { get; set; }
    public Sku Sku { get; set; } = null!;
    
    public Guid LanguageId { get; set; }
    public Language Language { get; set; } = null!;
    
    [StringLength(AppConst.MaxTranslationNameLength)]
    public string Name { get; set; } = null!;
    
    [StringLength(AppConst.MaxTranslationDescriptionLength)]
    public string Description { get; set; } = null!;
}