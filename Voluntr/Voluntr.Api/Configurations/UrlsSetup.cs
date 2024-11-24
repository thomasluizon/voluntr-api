using Voluntr.Domain.Config;

namespace Voluntr.Api.Configurations
{
    public static class UrlsSetup
    {
        public static void AddUrlsSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var urls = new Urls();
            configuration.Bind("Urls", urls);

            services.AddSingleton(urls);
        }
    }
}
