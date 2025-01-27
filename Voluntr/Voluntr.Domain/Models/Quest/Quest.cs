using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class Quest : Entity
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int Reward { get; set; }
        public int MaxVolunteers { get; set; }
        public bool IsRemote { get; set; }

        public virtual Project Project { get; set; }
    }
}
