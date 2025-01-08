using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Queries
{
    public class GetVolunteerProfileQuery : VolunteerQuery<VolunteerProfileDto>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
