using Microsoft.AspNetCore.Http;
using Voluntr.Crosscutting.Domain.Commands;

namespace Voluntr.Domain.Commands
{
    public abstract class UserCommand : Command
    {
        public IFormFile Picture { get; set; }
    }
}
