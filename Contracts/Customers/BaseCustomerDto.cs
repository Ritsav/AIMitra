using System.ComponentModel.DataAnnotations;
using Shared.Custom;

namespace Contracts.Customers;

public abstract class BaseCustomerDto
{
    [Required]
    [Range(AppConst.MinPriceRange, AppConst.MaxPriceRange)]
    public decimal BorrowedAmount { get; set; }
    
    public string? Address { get; set; }
    
    [Required]
    public ICollection<CustomerTranslationDto> Translations { get; set; } = [];
}