using Voluntr.Crosscutting.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Commands
{
    public abstract class ProjectCommand : CommandResponse<CommandResponseDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
