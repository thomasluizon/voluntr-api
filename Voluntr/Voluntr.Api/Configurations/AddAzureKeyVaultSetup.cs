using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Voluntr.Api.Configurations
{
    public static class KeyVaultExtensions
    {
        public static void AddAzureKeyVaultSetup(this WebApplicationBuilder builder)
        {
            var keyVaultSection = builder.Configuration.GetSection("KeyVault");

            var keyVaultUrl = Environment.GetEnvironmentVariable(keyVaultSection.GetValue<string>("KeyVaultUrl"));
            var tenantId = Environment.GetEnvironmentVariable(keyVaultSection.GetValue<string>("TenantId"));
            var clientId = Environment.GetEnvironmentVariable(keyVaultSection.GetValue<string>("ClientId"));
            var clientSecret = Environment.GetEnvironmentVariable(keyVaultSection.GetValue<string>("ClientSecret"));

            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

            var client = new SecretClient(new Uri(keyVaultUrl), credential);

            builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
        }
    }
}
