using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class Project : Entity
    {
        public Guid NgoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }

        public virtual ICollection<Quest> Quests { get; set; } = [];

        public virtual Ngo Ngo { get; set; }
    }
}
