using Voluntr.Domain.Services.Email;

namespace Voluntr.Api.Configurations
{
    public static class SendGridSetup
    {
        public static void AddSendGridSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new SendGridConfig();
            configuration.Bind("SendGrid", config);

            services.AddSingleton(config);
        }
    }
}
