using MediatR;
using Voluntr.Crosscutting.Domain.Commands.Events;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;

namespace Voluntr.Crosscutting.Domain.Events
{
    public abstract class Event : CommandMessage, INotification
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToBrazilianTimezone();
    }
}
