using Domain.SKUs;
using Shared.Custom.Bases;

namespace Domain.Products;

public class Product : AuditedEntity
{
    public List<Sku> Skus { get; set; } = [];
    public List<ProductTranslation> Translations { get; set; } = [];
}