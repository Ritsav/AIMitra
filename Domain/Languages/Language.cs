using System.ComponentModel.DataAnnotations;
using Shared.Custom;

namespace Domain.Languages;

public class Language
{
    public Guid Id { get; set; }
    
    [StringLength(AppConst.MaxLanguageCodeLength)]
    public string Code { get; set; } = null!;
    
    [StringLength(AppConst.MaxLanguageNameLength)]
    public string Name { get; set; } = null!;
}