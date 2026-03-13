using Microsoft.EntityFrameworkCore;
using Shared.Custom;
using Shared.Custom.Bases;

namespace Domain.Customers;

public class Customer : AuditedEntity
{
    [Precision(AppConst.DecimalPrecisionDigits, AppConst.DecimalPrecisionPoints)]
    public decimal BorrowedAmount { get; set; }
    
    // TODO: Add Address entity and relation later
    public string? Address { get; set; } 
    
    public List<CustomerTranslation> Translations { get; set; } = [];
}