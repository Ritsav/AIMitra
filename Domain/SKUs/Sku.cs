using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Custom;
using Shared.Custom.Bases;

namespace Domain.SKUs;

public class Sku : AuditedEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    [Precision(AppConst.DecimalPrecisionDigits, AppConst.DecimalPrecisionPoints)]
    public decimal Price { get; set; }
    
    public uint Quantity { get; set; }

    public List<SkuTranslation> Translations { get; set; } = [];
}