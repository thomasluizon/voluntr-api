using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class DeleteProjectValidation : ProjectValidation<DeleteProjectCommand>
    {
        public DeleteProjectValidation()
        {
            ValidateId();
        }
    }
}
