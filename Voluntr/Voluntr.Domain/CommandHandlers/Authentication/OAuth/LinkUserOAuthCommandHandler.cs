using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;

namespace Voluntr.Domain.CommandHandlers
{
    public class LinkUserOAuthCommandHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        IUserRepository userRepository,
        IOAuthProviderRepository oAuthProviderRepository,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<LinkUserOAuthCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(LinkUserOAuthCommand request)
        {
            var user = await userRepository.GetFirstByExpressionAsync(
                x => x.Id == claimsService.GetCurrentUserId(),
                x => x.OAuthProvider
            );

            if (user == null)
            {
                NotifyError("O usuário informado não foi encontrado");
                return null;
            }

            if (user.OAuthProvider?.Name == request.OAuthProviderName)
            {
                user.OAuthProviderId = null;
            }
            else
            {
                var newOAuthProvider = await oAuthProviderRepository.GetFirstByExpressionAsync(
                    x => x.Name == request.OAuthProviderName
                );

                if (newOAuthProvider == null)
                {
                    NotifyError("O provedor de autenticação informado não foi encontrado");
                    return null;
                }

                user.OAuthProviderId = newOAuthProvider.Id;
            }

            await userRepository.UpdateAsync(user);

            if (!HasNotification() && await unitOfWork.CommitAsync())
                request.ExecutedSuccessfullyCommand = true;
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}
