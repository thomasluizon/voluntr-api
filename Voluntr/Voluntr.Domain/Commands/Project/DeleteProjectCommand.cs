using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class DeleteProjectCommand : ProjectCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new DeleteProjectValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
