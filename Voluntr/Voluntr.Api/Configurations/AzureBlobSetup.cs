using Voluntr.Crosscutting.Domain.Services.Storage;

namespace Voluntr.Api.Configurations
{
    public static class AzureBlobSetup
    {
        public static void AddAzureBlobSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new StorageConfig
            {
                ConnectionString = configuration.GetConnectionString("BlobStorage")
            };

            services.AddSingleton(config);
        }
    }
}
