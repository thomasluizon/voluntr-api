using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class ResetPasswordRequestValidation : AccountValidation<ResetPasswordRequestCommand, AuthenticationDto>
    {
        public ResetPasswordRequestValidation()
        {
            ValidateEmail();
        }
    }
}
