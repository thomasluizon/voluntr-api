using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class Address : Entity
    {
        public Guid UserId { get; set; }

        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighbourhood { get; set; }
        public string Uf { get; set; }
        public string City { get; set; }

        public virtual User User { get; set; }
    }
}
