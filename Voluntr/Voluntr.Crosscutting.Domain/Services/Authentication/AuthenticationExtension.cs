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

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                });
        }
    }
}
