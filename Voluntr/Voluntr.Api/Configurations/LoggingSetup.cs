using Microsoft.Extensions.Logging.ApplicationInsights;

namespace Voluntr.Api.Configurations
{
    public static class LoggingSetup
    {
        public static void AddLoggingSetup(this WebApplicationBuilder builder)
        {
            var appInsightsConnectionString = builder.Configuration.GetConnectionString("ApplicationInsights");

            if (string.IsNullOrEmpty(appInsightsConnectionString)) 
                return;

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Logging.AddApplicationInsights(
                configureTelemetryConfiguration: (config) => config.ConnectionString = appInsightsConnectionString,
                configureApplicationInsightsLoggerOptions: (options) => { }
            );

            builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);

            builder.Services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = appInsightsConnectionString;
                options.EnableAdaptiveSampling = false;
            });
        }
    }
}
