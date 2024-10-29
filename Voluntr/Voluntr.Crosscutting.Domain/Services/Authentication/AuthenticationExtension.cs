using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Voluntr.Crosscutting.Domain.Services.Authentication
{
    public static class AuthenticationExtension
    {
        public static AuthenticationBuilder AddVoluntrAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfig = new TokenConfig();
            configuration.GetSection("Authentication:TokenCredentials").Bind(tokenConfig);

            var azureAdB2CConfig = new AzureAdB2CConfig();
            configuration.GetSection("Authentication:AzureAdB2C").Bind(azureAdB2CConfig);

            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "AzureAdB2C";
            })
            .AddJwtBearer(options =>
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
            })
            .AddCookie()
            .AddOpenIdConnect("AzureAdB2C", options =>
            {
                options.Authority = $"{azureAdB2CConfig.Instance}{azureAdB2CConfig.Domain}/{azureAdB2CConfig.SignUpSignInPolicyId}/v2.0";
                options.ClientId = azureAdB2CConfig.ClientId;
                options.ClientSecret = azureAdB2CConfig.ClientSecret;
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.CallbackPath = azureAdB2CConfig.CallbackPath;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    ValidateIssuer = true,
                };
            });

            return authenticationBuilder;
        }
    }
}
