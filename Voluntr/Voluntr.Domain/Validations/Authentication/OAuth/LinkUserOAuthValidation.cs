using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class LinkUserOAuthValidation : AccountValidation<LinkUserOAuthCommand, AuthenticationDto>
    {
        public LinkUserOAuthValidation()
        {
            ValidateOAuthProvider();
        }
    }
}
