using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class UploadPictureValidation : UserValidation<UploadPictureCommand>
    {
        public UploadPictureValidation()
        {
            ValidatePicture();
        }
    }
}
