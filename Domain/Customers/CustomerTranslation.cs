using System.ComponentModel.DataAnnotations;
using Domain.Languages;
using Shared.Custom.Bases;
using Shared.Languages;
using Shared.Products;

namespace Domain.Customers;

public class CustomerTranslation : BaseEntity, IHasTranslation
{
    public Guid LanguageId { get; set; }
    public Language Language { get; set; } = null!;
    
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    [StringLength(ProductConst.MaxNameLength)]
    public string Name { get; set; } = null!;
}