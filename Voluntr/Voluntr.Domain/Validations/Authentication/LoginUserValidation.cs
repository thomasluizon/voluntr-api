using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class LoginUserValidation : AccountValidation<LoginUserCommand, AuthenticationDto>
    {
        public LoginUserValidation()
        {
            ValidateEmail();
            ValidatePassword();
        }
    }
}
