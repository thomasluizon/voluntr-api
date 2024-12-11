using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class RegisterUserValidation : AccountValidation<RegisterUserCommand, CommandResponseDto>
    {
        public RegisterUserValidation()
        {
            ValidateEmail();
            ValidatePassword();
            ValidateName();
            ValidateRegister();
            ValidateAddress();
        }
    }
}
