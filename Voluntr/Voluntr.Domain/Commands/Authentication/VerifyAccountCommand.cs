using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class VerifyAccountCommand : AccountCommand<CommandResponseDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new VerifyAccountValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
