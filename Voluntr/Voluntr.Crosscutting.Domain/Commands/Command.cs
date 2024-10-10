using FluentValidation.Results;
using System.Text.Json.Serialization;
using Voluntr.Crosscutting.Domain.Commands.Events;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;

namespace Voluntr.Crosscutting.Domain.Commands
{
    public abstract class Command : CommandMessage
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime Timestamp { get; private set; } = DateTime.Now.ToBrazilianTimezone();

        [JsonIgnore]
        public bool ExecutedSuccessfullyCommand { get; set; } = false;

        [JsonIgnore]
        public string ActionPerformedUserId { get; set; }

        [JsonIgnore]
        public string Route { get; set; }

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; } = new();

        public abstract bool IsValid();
    }
}
