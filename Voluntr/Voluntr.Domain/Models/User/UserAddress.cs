using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class UserAddress : Entity
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }

        public virtual User User { get; set; }
        public virtual Address Address { get; set; }
    }
}
