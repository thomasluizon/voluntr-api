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

namespace Voluntr.Domain.CommandHandlers
{
    public class HandleGoogleCallbackCommandHandler(
        IMediatorHandler mediator,
        IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository,
        IClaimsService claimsService,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<HandleGoogleCallbackCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(HandleGoogleCallbackCommand request)
        {
            var authenticateResult = await httpContextAccessor.HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                NotifyError("Falha ao autenticar via Google");
                return null;
            }

            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);

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
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}
