using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class Ngo : Entity
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
