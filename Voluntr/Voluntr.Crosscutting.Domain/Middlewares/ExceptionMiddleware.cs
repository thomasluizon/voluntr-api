using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Voluntr.Crosscutting.Domain.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var loggerFactory = context.RequestServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
                        var logger = loggerFactory.CreateLogger("ExceptionMiddleware");

                        logger.LogError(
                            contextFeature.Error,
                            "Erro não tratado ocorrido no caminho {Path}. Mensagem: {ErrorMessage}",
                            context.Request.Path,
                            contextFeature.Error.Message
                        );
                    }

                    await context.Response.WriteAsync("Nossos servidores estão indisponíveis no momento. Por favor, tente mais tarde.");
                });
            });
        }
    }
}
