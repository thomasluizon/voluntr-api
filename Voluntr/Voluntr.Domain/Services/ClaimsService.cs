using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Helpers.Validators;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Crosscutting.Domain.Services.Authentication;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.Services
{
    public class ClaimsService(
       ICryptographyService cryptographyService,
       ClaimsPrincipal claims,
       IMediatorHandler mediator,
       TokenConfig tokenConfig
    ) : IClaimsService
    {
        public Guid? GetCurrentUserId()
        {
            var userId = claims.GetUserIdFromToken();

            if (string.IsNullOrEmpty(userId))
                return null;

            userId = cryptographyService.Decrypt(userId);

            if (!Validator.IsGuid(userId))
            {
                NotifyError(Values.Message.UserRequestNotFound);
                return null;
            }

            return Guid.Parse(userId);
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, cryptographyService.Encrypt(user.Id.ToString())),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: tokenConfig.Issuer,
                audience: tokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(tokenConfig.ExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        protected void NotifyError(string message) => NotifyError(string.Empty, message);

        protected void NotifyError(string code, string message)
        {
            mediator.PublishEvent(new DomainNotification(code, message));
        }
    }
}
