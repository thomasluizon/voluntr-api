using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Validations
{
    public class VerifyAccountValidation : AccountValidation<VerifyAccountCommand, CommandResponseDto>
    {
        public VerifyAccountValidation()
        {
            ValidateToken();
        }
    }
}
