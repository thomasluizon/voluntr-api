using Voluntr.Crosscutting.Domain.Commands;

namespace Voluntr.Domain.Commands
{
    public abstract class ProjectCommand<TResponse> : CommandResponse<TResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
