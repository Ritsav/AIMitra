using System.ComponentModel.DataAnnotations;

namespace Contracts.Users;

public class UserInfoRequestDto : IValidatableObject
{
    [EmailAddress]
    public string? Email { get; set; }

    public string? Username { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(Username == null && Email == null)
        {
            yield return new ValidationResult(
                "Either Email or Username must be provided.",
                [nameof(Email), nameof(Username)]);
        }
    }
}