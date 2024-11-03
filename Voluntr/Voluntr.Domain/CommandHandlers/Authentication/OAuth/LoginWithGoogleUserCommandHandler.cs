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
    public class LoginWithGoogleUserCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        IClaimsService claimsService,
        IOAuthService OAuthService,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<LoginWithGoogleUserCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(LoginWithGoogleUserCommand request)
        {
            var googleUser = await OAuthService.ValidateGoogleTokenAsync(request.GoogleToken);

            if (googleUser == null)
            {
                NotifyError("Falha ao validar o token do Google.");
                return null;
            }

            var user = await userRepository.GetFirstByExpressionAsync(x => x.Email == googleUser.Email.Trim());

            if (user == null)
            {
                user = new User
                {
                    Email = googleUser.Email.Trim(),
                    Name = googleUser.Name.Trim(),
                    OAuth = true,
                    OAuthProviderEnum = OAuthProviderEnum.Google
                };

                // Lógica de foto de perfil, fazer o update caso esteja diferente la na azure

                await userRepository.InsertAsync(user);
            }
            else if (!user.OAuth)
            {
                user.OAuth = true;
                user.OAuthProviderEnum = OAuthProviderEnum.Google;

                // Lógica de foto de perfil, fazer o update caso esteja diferente la na azure

                await userRepository.UpdateAsync(user);
            }
            else if (user.OAuthProviderEnum != OAuthProviderEnum.Google)
            {
                NotifyError("Já existe um usuário com o email informado vinculado a outro provedor.");
                return null;
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
