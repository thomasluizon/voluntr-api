using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class LoginUserValidation : AuthenticationValidation<LoginUserCommand, AuthenticationDto>
    {
        public LoginUserValidation()
        {
            ValidateEmail();
            ValidatePassword();
        }
    }
}
