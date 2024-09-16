using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Queries
{
    public class GetVolunteersQuery : VolunteerQuery<List<VolunteerDto>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
