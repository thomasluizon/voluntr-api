using Voluntr.Crosscutting.Domain.Services.Authentication;

namespace Voluntr.Api.Configurations
{
    public static class TokenCredentialSetup
    {
        public static void AddTokenCredentialSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var tokenConfig = new TokenConfig();
            configuration.Bind("Authentication:TokenCredentials", tokenConfig);

            var googleConfig = new GoogleConfig();
            configuration.Bind("Authentication:Google", googleConfig);

            services.AddSingleton(tokenConfig);
            services.AddSingleton(googleConfig);
        }
    }
}
