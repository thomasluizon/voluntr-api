using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class RegisterUserValidation : AuthenticationValidation<RegisterUserCommand, CommandResponseDto>
    {
        public RegisterUserValidation()
        {
            ValidateEmail();
            ValidatePassword();
            ValidateName();
        }
    }
}
