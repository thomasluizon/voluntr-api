using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class OAuthLoginUserCommand : AccountCommand<AuthenticationDto>
    {
        public override bool IsValid()
        {
            ValidationResult = new OAuthLoginUserValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}