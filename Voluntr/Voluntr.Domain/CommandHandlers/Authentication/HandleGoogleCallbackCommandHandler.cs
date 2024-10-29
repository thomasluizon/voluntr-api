using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Domain.Models;
using Microsoft.Identity.Client;

namespace Voluntr.Domain.CommandHandlers
{
    public class HandleGoogleCallbackCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        IClaimsService claimsService,
        IUnitOfWork unitOfWork,
        IConfidentialClientApplication confidentialClientApplication // Assumindo que você configurou esse client para troca de token
    ) : MediatorResponseCommandHandler<HandleGoogleCallbackCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(HandleGoogleCallbackCommand request)
        {
            AuthenticationResult authResult;
            try
            {
                authResult = await confidentialClientApplication.AcquireTokenByAuthorizationCode(new[] { "User.Read" }, request.Code).ExecuteAsync();
            }
            catch (MsalServiceException ex)
            {
                NotifyError("Erro ao trocar o código de autorização pelo token: " + ex.Message);
                return null;
            }

            var idToken = authResult.IdToken;
            var claimsPrincipal = ParseIdToken(idToken);

            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var name = claimsPrincipal.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(email))
            {
                NotifyError("Email não encontrado no token recebido");
                return null;
            }

            var user = await userRepository.GetFirstByExpressionAsync(x => x.Email == email);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    Name = name,
                    Password = null,
                    OAuth = true,
                    OAuthProviderEnum = OAuthProviderEnum.Google
                };

                await userRepository.InsertAsync(user);
            }

            if (!HasNotification() && await unitOfWork.CommitAsync())
            {
                request.ExecutedSuccessfullyCommand = true;

                return new AuthenticationDto
                {
                    AccessToken = claimsService.GenerateToken(user)
                };
            }
            else
            {
                NotifyError(Values.Message.DefaultError);
            }

            return null;
        }

        private static ClaimsPrincipal ParseIdToken(string idToken)
        {
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
            return new ClaimsPrincipal(new ClaimsIdentity(jsonToken.Claims, "jwt"));
        }
    }
}
