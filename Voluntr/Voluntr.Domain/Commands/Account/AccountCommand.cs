using Voluntr.Crosscutting.Domain.Commands;

namespace Voluntr.Domain.Commands
{
    public abstract class AccountCommand<TResponse> : CommandResponse<TResponse>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public string Token { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthProviderName { get; set; }
    }
}
