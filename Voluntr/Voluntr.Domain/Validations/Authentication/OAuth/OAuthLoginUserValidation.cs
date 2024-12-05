using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class OAuthLoginUserValidation : AccountValidation<OAuthLoginUserCommand, AuthenticationDto>
    {
        public OAuthLoginUserValidation()
        {
            ValidateOAuthProvider();
            ValidateOAuthToken();
        }
    }
}
