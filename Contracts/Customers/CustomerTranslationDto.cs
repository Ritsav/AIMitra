using System.ComponentModel.DataAnnotations;

namespace Contracts.Customers;

public class CustomerTranslationDto
{
    [Required]
    public Guid LanguageId { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}