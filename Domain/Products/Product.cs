using System.Collections.ObjectModel;
using Shared.Custom.Bases;

namespace Domain.Products;

public class Product : AuditedEntity
{
    public ICollection<ProductTranslation> Translations { get; set; } =
        new Collection<ProductTranslation>();
}