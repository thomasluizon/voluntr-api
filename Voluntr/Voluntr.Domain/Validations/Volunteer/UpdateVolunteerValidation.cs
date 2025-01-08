using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations
{
    public class UpdateVolunteerValidation : VolunteerValidation<UpdateVolunteerCommand>
    {
        public UpdateVolunteerValidation()
        {
            ValidateVolunteer();
        }
    }
}
