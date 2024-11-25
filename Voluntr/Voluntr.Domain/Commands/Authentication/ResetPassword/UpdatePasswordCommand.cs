using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class UpdatePasswordCommand : AuthenticationCommand<AuthenticationDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new UpdatePasswordValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
