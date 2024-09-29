using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Voluntr.Api.Configurations
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddSwaggerGen(c =>
            {
                // Inclui comentários XML
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Definição do esquema de segurança JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // Requisito de segurança global
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

                // Configurações da documentação da API
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Documentação API Voluntr",
                    Description = "API de serviços voltados à plataforma Voluntr",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe Voluntr",
                        Email = "thomaslrgregorio@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                // Agrupa as ações pelos controladores
                c.TagActionsBy(api =>
                {
                    return new List<string> { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] };
                });

                // Outras configurações, se necessário
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            // Gera o documento Swagger JSON
            app.UseSwagger();

            // Configura o Swagger UI
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Voluntr API v1");
            });

            // Configura o ReDoc usando Swashbuckle
            app.UseReDoc(c =>
            {
                c.RoutePrefix = "docs";
                c.SpecUrl = "/swagger/v1/swagger.json";
                c.DocumentTitle = "Voluntr API Documentation";
            });
        }
    }
}
