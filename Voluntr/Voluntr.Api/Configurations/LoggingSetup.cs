using Microsoft.ApplicationInsights.Extensibility;

namespace Voluntr.Api.Configurations
{
    public static class LoggingSetup
    {
        public static void AddLoggingSetup(this WebApplicationBuilder builder)
        {
            var appInsightsConnectionString = builder.Configuration.GetValue<string>("APPLICATIONINSIGHTS_CONNECTION_STRING");

            if (string.IsNullOrEmpty(appInsightsConnectionString))
                return;

            builder.Services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = appInsightsConnectionString;
            });

            builder.Logging.AddApplicationInsights(
                configureTelemetryConfiguration: (config) =>
                {
                    config.ConnectionString = appInsightsConnectionString;
                },
                configureApplicationInsightsLoggerOptions: (options) => { }
            );

            builder.Services.AddSingleton<ITelemetryInitializer, OperationCorrelationTelemetryInitializer>();
        }
    }
}
