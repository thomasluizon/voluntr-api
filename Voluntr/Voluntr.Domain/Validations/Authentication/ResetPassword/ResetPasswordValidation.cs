using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class ResetPasswordValidation : AccountValidation<ResetPasswordCommand, AuthenticationDto>
    {
        public ResetPasswordValidation()
        {
            ValidateToken();
            ValidatePassword();
        }
    }
}
