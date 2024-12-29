using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Queries.Achievement
{
    public class GetCauseAchievementsPageQuery : AchievementQuery<CauseAchievementsPageDto>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
