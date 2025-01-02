using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class UpdateProjectCommand : ProjectCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new UpdateProjectValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
