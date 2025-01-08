using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Queries
{
    public class GetVolunteerOnboardingQuery : VolunteerQuery<List<OnboardingTaskDto>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
