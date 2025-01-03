using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class AddProjectCommand : ProjectCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new AddProjectValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
