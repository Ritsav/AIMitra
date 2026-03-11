using System.ComponentModel.DataAnnotations;

namespace Shared.Custom.Bases;

public abstract class BaseTranslationDto
{
    [Required]
    public Guid LanguageId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
}