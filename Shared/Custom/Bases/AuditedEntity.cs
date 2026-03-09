namespace Shared.Custom.Bases;

public class AuditedEntity : BaseEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}