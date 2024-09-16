using MediatR;
using System.Text.Json.Serialization;
using Voluntr.Crosscutting.Domain.Events;

namespace Voluntr.Crosscutting.Domain.Commands.Events
{
    public abstract class CommandMessage : IRequest, IRequestBase
    {
        [JsonIgnore]
        public string MessageType { get; protected set; }

        protected CommandMessage()
        {
            MessageType = GetType().Name;
        }
    }
}
