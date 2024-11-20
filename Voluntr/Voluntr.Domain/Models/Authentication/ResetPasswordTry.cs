using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class ResetPasswordTry : Entity
    {
        public Guid UserId { get; set; }
        public string ResetToken { get; set; }

        public virtual User User { get; set; }
    }
}
