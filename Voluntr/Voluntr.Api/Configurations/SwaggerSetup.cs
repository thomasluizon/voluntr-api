using NSwag;
using System.Reflection;

namespace Voluntr.Api.Configurations
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerDocument(config =>
            {
                config.UseControllerSummaryAsTagDescription = true;

                config.AddSecurity("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT no formato Bearer {token}",
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Documentação API Voluntr";
                    document.Info.Description = "API de serviços voltados a plataforma Voluntr";
                };
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UseOpenApi();
            app.UseReDoc(opt =>
            {
                opt.Path = "/docs";
            });

            app.UseSwagger(u =>
            {
                u.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Voluntr");
            });
        }
    }
}
