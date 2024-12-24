using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class AchievementCategory : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; } = [];
    }
}