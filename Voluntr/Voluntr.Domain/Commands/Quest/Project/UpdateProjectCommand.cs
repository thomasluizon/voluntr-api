using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class UpdateProjectCommand : ProjectCommand<CommandResponseDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new UpdateProjectValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
