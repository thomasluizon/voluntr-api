using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class LinkUserOAuthValidation : AuthenticationValidation<LinkUserOAuthCommand, AuthenticationDto>
    {
        public LinkUserOAuthValidation()
        {
            ValidateOAuthProvider();
        }
    }
}
