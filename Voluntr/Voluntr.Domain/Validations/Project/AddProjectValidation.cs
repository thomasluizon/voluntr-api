using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class AddProjectValidation : ProjectValidation<AddProjectCommand>
    {
        public AddProjectValidation()
        {
            ValidateProject();
        }
    }
}
