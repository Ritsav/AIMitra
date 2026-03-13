namespace Shared.Languages;

public interface IHasTranslation
{
    public Guid LanguageId { get; set; }
    
    public string Name { get; set; }
}