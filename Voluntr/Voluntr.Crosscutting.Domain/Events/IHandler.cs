using Voluntr.Crosscutting.Domain.Commands.Events;

namespace Voluntr.Crosscutting.Domain.Events
{
    public interface IHandler<in T> where T : CommandMessage
    {
        void Handle(T message);
    }
}
