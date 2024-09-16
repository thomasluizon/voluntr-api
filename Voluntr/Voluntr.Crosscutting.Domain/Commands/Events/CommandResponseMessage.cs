using MediatR;
using Voluntr.Crosscutting.Domain.Events;

namespace Voluntr.Crosscutting.Domain.Commands.Events
{
    public class CommandResponseMessage<TResponse> : IRequest<TResponse>, IRequestBase
    {
        public string MessageType { get; protected set; }

        protected CommandResponseMessage()
        {
            MessageType = GetType().Name;
        }
    }
}
