using Voluntr.Crosscutting.Domain.Events;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.Events
{
    public class EmailActivationEvent : Event
    {
        public User User { get; set; }
        public string EmailActivationToken { get; set; }
    }
}
