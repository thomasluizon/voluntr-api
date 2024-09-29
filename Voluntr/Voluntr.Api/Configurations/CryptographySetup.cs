using Voluntr.Crosscutting.Domain.Services.Criptography;

namespace Voluntr.Api.Configurations
{
    public static class CryptographySetup
    {
        public static void AddCryptographySetup(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new CryptographyConfig();
            configuration.Bind("Cryptography", config);

            services.AddSingleton(config);
        }
    }
}
