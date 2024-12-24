using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class UserAchievement : Entity
    {
        public Guid UserId { get; set; }
        public Guid AchievementId { get; set; }

        public virtual User User { get; set; }
        public virtual Achievement Achievement { get; set; }
    }
}
