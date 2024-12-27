using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class UserCause : Entity
    {
        public Guid UserId { get; set; }
        public Guid CauseId { get; set; }

        public virtual User User { get; set; }
        public virtual Cause Cause { get; set; }
    }
}
