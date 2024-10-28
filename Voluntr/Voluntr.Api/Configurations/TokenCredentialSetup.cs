using Voluntr.Crosscutting.Domain.Services.Authentication;

namespace Voluntr.Api.Configurations
{
    public static class TokenCredentialSetup
    {
        public static void AddTokenCredentialSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfig = new TokenConfig();
            configuration.Bind("Authentication:TokenCredentials", tokenConfig);

            services.AddSingleton(tokenConfig);
        }
    }
}
