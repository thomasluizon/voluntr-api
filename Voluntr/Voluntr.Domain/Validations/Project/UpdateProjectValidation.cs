using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class UpdateProjectValidation : ProjectValidation<UpdateProjectCommand>
    {
        public UpdateProjectValidation()
        {
            ValidateId();
            ValidateProject();
        }
    }
}
