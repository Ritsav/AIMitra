using System.ComponentModel.DataAnnotations;

namespace Contracts.Products;

public class ProductTranslationDto
{
    [Required]
    public Guid LanguageId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
}