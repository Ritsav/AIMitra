using System.Collections.ObjectModel;
using Domain.SKUs;
using Shared.Custom.Bases;

namespace Domain.Products;

public class Product : AuditedEntity
{
    public ICollection<Sku> Skus { get; set; } = new Collection<Sku>();
    public ICollection<ProductTranslation> Translations { get; set; } =
        new Collection<ProductTranslation>();
}