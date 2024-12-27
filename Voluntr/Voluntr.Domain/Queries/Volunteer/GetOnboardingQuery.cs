using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Queries
{
    public class GetOnboardingQuery : VolunteerQuery<List<OnboardingTaskDto>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
