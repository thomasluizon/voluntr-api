using FluentValidation.Results;
using System.Text.Json.Serialization;
using Voluntr.Crosscutting.Domain.Queries.Events;

namespace Voluntr.Crosscutting.Domain.Queries
{
    public abstract class Query<TResponse> : QueryMessage<TResponse>
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public string Route { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public ValidationResult ValidationResult { get; set; } = new();

        public abstract bool IsValid();
    }
}
