using Voluntr.Crosscutting.Domain.Services.Storage;

namespace Voluntr.Api.Configurations
{
    public static class AzureBlobSetup
    {
        public static void AddAzureBlobSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var config = new StorageConfig
            {
                ConnectionString = configuration.GetConnectionString("AzureBlob")
            };

            services.AddSingleton(config);
        }
    }
}
