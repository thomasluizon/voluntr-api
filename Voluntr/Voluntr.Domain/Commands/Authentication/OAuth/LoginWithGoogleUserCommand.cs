using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class LoginWithGoogleUserCommand : AuthenticationCommand<AuthenticationDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new LoginWithGoogleUserValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
