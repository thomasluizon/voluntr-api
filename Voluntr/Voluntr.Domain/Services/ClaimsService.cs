﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Voluntr.Crosscutting.Domain.Events.Notifications;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Helpers.Validators;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Crosscutting.Domain.Services.Authentication;
using Voluntr.Domain.Enumerators;
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

        public string GetCurrentUserType()
        {
            var userType = claims.GetUserTypeFromToken();

            if (string.IsNullOrEmpty(userType))
            {
                NotifyError(Values.Message.UserTypeNotFound);
                return null;
            }

            return userType;
        }

        public string GenerateAuthToken(User user, UserTypeEnum userType)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, cryptographyService.Encrypt(user.Id.ToString())),
                new Claim(JwtRegisteredClaimNames.Profile, userType.GetDescription()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            JwtSecurityToken token;

            if (tokenConfig.ExpiryMinutes == 0)
            {
                token = new JwtSecurityToken(
                    issuer: tokenConfig.Issuer,
                    audience: tokenConfig.Audience,
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.ToBrazilianTimezone().AddYears(200)
                );
            }
            else
            {
                token = new JwtSecurityToken(
                    issuer: tokenConfig.Issuer,
                    audience: tokenConfig.Audience,
                    claims: claims,
                    expires: DateTime.Now.ToBrazilianTimezone().AddMinutes(tokenConfig.ExpiryMinutes),
                    signingCredentials: credentials
                );
            }

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateGenericToken(User user, int expiryMinutes = 15)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", cryptographyService.Encrypt(user.Id.ToString())),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: tokenConfig.Issuer,
                audience: tokenConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsTokenValid(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(tokenConfig.Secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = tokenConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = tokenConfig.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Guid? GetUserIdFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                NotifyError("Token is required.");
                return null;
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();

                if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
                {
                    NotifyError("Invalid token format.");
                    return null;
                }

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    NotifyError("User ID not found in token.");
                    return null;
                }

                var decryptedUserId = cryptographyService.Decrypt(userIdClaim);

                if (!Validator.IsGuid(decryptedUserId))
                {
                    NotifyError(Values.Message.UserRequestNotFound);
                    return null;
                }

                return Guid.Parse(decryptedUserId);
            }
            catch (Exception ex)
            {
                NotifyError($"Error parsing token: {ex.Message}");
                return null;
            }
        }

        protected void NotifyError(string message) => NotifyError(string.Empty, message);

        protected void NotifyError(string code, string message)
        {
            mediator.PublishEvent(new DomainNotification(code, message));
        }
    }
}
