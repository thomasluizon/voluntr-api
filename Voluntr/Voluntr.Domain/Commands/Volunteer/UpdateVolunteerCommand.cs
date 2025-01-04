using Voluntr.Domain.Validations.Volunteer;

namespace Voluntr.Domain.Commands
{
    public class UpdateVolunteerCommand : VolunteerCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new UpdateVolunteerValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
