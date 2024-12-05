using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class UpdatePasswordValidation : AccountValidation<UpdatePasswordCommand, AuthenticationDto>
    {
        public UpdatePasswordValidation()
        {
            ValidatePassword();
            ValidateNewPassword();
        }
    }
}
