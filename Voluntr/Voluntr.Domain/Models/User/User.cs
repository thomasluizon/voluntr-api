using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class User : Entity
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
}
