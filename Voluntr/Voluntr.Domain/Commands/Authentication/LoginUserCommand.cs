using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class LoginUserCommand : AccountCommand<AuthenticationDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new LoginUserValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
