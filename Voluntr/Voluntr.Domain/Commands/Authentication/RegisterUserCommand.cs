using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class RegisterUserCommand : AuthenticationCommand<CommandResponseDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new RegisterUserValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
