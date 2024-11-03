using Voluntr.Crosscutting.Domain.Commands;

namespace Voluntr.Domain.Commands
{
    public abstract class AuthenticationCommand<TResponse> : CommandResponse<TResponse>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public string GoogleToken { get; set; }
    }
}
