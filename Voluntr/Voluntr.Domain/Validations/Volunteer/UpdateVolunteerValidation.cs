using Voluntr.Domain.Commands;

namespace Voluntr.Domain.Validations.Volunteer
{
    public class UpdateVolunteerValidation : VolunteerValidation<UpdateVolunteerCommand>
    {
        public UpdateVolunteerValidation()
        {
            ValidateVolunteer();
        }
    }
}
