using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class LoginWithGoogleUserValidation : AuthenticationValidation<LoginWithGoogleUserCommand, AuthenticationDto>
    {
        public LoginWithGoogleUserValidation()
        {
            ValidateGoogleToken();
        }
    }
}
