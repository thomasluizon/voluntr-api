using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class ResetPasswordRequestCommand : AuthenticationCommand<AuthenticationDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new ResetPasswordRequestValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
