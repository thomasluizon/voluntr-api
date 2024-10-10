using Voluntr.Crosscutting.Domain.Helpers.Extensions;

namespace Voluntr.Crosscutting.Domain.Models
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToBrazilianTimezone();
        public DateTime? UpdatedAt { get; set; }
    }
}
