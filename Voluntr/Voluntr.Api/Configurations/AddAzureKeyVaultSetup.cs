using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Voluntr.Api.Extensions
{
    public static class KeyVaultExtensions
    {
        public static void AddAzureKeyVaultSetup(this WebApplicationBuilder builder)
        {
            var isDevelopment = builder.Environment.IsDevelopment();

            var keyVaultUrl = isDevelopment
                ? Environment.GetEnvironmentVariable("VOLUNTR_KEY_VAULT_URL_BETA")
                : Environment.GetEnvironmentVariable(builder.Configuration["KeyVault:KeyVaultUrl"]);

            var tenantId = isDevelopment
                ? Environment.GetEnvironmentVariable("VOLUNTR_TENANT_ID_BETA")
                : Environment.GetEnvironmentVariable(builder.Configuration["KeyVault:TenantId"]);

            var clientId = isDevelopment
                ? Environment.GetEnvironmentVariable("VOLUNTR_CLIENT_ID_BETA")
                : Environment.GetEnvironmentVariable(builder.Configuration["KeyVault:ClientId"]);

            var clientSecret = isDevelopment
                ? Environment.GetEnvironmentVariable("VOLUNTR_CLIENT_SECRET_BETA")
                : Environment.GetEnvironmentVariable(builder.Configuration["KeyVault:ClientSecret"]);

            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var client = new SecretClient(new Uri(keyVaultUrl), credential);

            builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
        }
    }
}
