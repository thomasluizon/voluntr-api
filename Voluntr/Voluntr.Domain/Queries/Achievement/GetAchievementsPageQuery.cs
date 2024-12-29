using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Queries.Achievement
{
    public class GetAchievementsPageQuery : AchievementQuery<AchievementsPageDto>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
