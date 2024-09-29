using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Voluntr.Crosscutting.Domain.Services.Authentication
{
    public static class AuthenticationExtension
    {
        public static AuthenticationBuilder AddVoluntrAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationSection = configuration.GetSection("Authentication");

            services.Configure<TokenConfig>(authenticationSection.GetSection("TokenCredentials"));
            //services.Configure<GoogleConfig>(authenticationSection.GetSection("Google"));

            services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtOptions>();
            //services.AddSingleton<IConfigureOptions<GoogleOptions>, ConfigureGoogleOptions>();

            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

            //authenticationBuilder.AddGoogle();

            return authenticationBuilder;
        }

        private class ConfigureJwtOptions : IConfigureNamedOptions<JwtBearerOptions>
        {
            private readonly TokenConfig tokenConfig;

            public ConfigureJwtOptions(IOptions<TokenConfig> tokenConfigOptions)
            {
                tokenConfig = tokenConfigOptions.Value;

                if (string.IsNullOrEmpty(tokenConfig.Secret))
                {
                    throw new Exception("Secret está indefinido. Verifique se 'JwtSettings:Secret' está configurado no appsettings.json.");
                }
            }

            public void Configure(string name, JwtBearerOptions options)
            {
                var key = Encoding.UTF8.GetBytes(tokenConfig.Secret);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = tokenConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = tokenConfig.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }

            public void Configure(JwtBearerOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }

        private class ConfigureGoogleOptions : IConfigureNamedOptions<GoogleOptions>
        {
            private readonly GoogleConfig googleConfig;

            public ConfigureGoogleOptions(IOptions<GoogleConfig> googleConfigOptions)
            {
                googleConfig = googleConfigOptions.Value;

                if (string.IsNullOrEmpty(googleConfig.ClientId) || string.IsNullOrEmpty(googleConfig.ClientSecret))
                {
                    throw new Exception("ClientId ou ClientSecret do Google estão indefinidos. Verifique se 'Authentication:Google:ClientId' e 'Authentication:Google:ClientSecret' estão configurados no appsettings.json.");
                }
            }

            public void Configure(string name, GoogleOptions options)
            {
                options.ClientId = googleConfig.ClientId;
                options.ClientSecret = googleConfig.ClientSecret;
                options.CallbackPath = googleConfig.CallbackPath;
            }

            public void Configure(GoogleOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}
