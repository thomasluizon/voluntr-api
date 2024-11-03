using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class OAuthLoginUserValidation : AuthenticationValidation<OAuthLoginUserCommand, AuthenticationDto>
    {
        public OAuthLoginUserValidation()
        {
            ValidateOAuth();
        }
    }
}
