using Voluntr.Domain.Validations;

namespace Voluntr.Domain.Commands
{
    public class UploadPictureCommand : UserCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new UploadPictureValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
