using MediatR;
using Voluntr.Crosscutting.Domain.Events;

namespace Voluntr.Crosscutting.Domain.Queries.Events
{
    public class QueryMessage<TResponse> : IRequest<TResponse>, IRequestBase
    {
        public string MessageType { get; set; }

        protected QueryMessage()
        {
            MessageType = GetType().Name;
        }
    }
}
