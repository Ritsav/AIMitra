using System.ComponentModel.DataAnnotations;
using Domain.Languages;
using Shared.Custom.Bases;
using Shared.Languages;
using Shared.Products;

namespace Domain.Products;

public class ProductTranslation : BaseEntity, IHasTranslation
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public Guid LanguageId { get; set; }
    public Language Language { get; set; } = null!;

    [StringLength(ProductConst.MaxNameLength)]
    public string Name { get; set; } = null!;

    [StringLength(ProductConst.MaxDescriptionLength)]
    public string Description { get; set; } = null!;
}