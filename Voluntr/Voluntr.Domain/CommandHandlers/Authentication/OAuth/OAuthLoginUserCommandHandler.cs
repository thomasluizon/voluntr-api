using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Events;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.CommandHandlers
{
    public class OAuthLoginUserCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        IClaimsService claimsService,
        IOAuthService OAuthService,
        IOAuthProviderRepository OAuthProviderRepository,
        IUnitOfWork unitOfWork,
        IVolunteerRepository volunteerRepository
    ) : MediatorResponseCommandHandler<OAuthLoginUserCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(OAuthLoginUserCommand request)
        {
            var OAuthProvider = await OAuthProviderRepository.GetFirstByExpressionAsync(
                x => x.Name == request.OAuthProviderName.Trim()
            );

            if (OAuthProvider == null)
            {
                NotifyError($"O provedor de autenticação {request.OAuthProviderName} não foi encontrado.");
                return null;
            }

            var OAuthUser = await OAuthService.ValidateOAuthTokenAsync(
                request.OAuthToken,
                OAuthProvider
            );

            if (OAuthUser == null)
            {
                NotifyError($"Falha ao validar o token do {request.OAuthProviderName}.");
                return null;
            }

            var user = await userRepository.GetFirstByExpressionAsync(x => x.Email == OAuthUser.Email.Trim());

            if (user == null)
            {
                user = new User
                {
                    Email = OAuthUser.Email.Trim(),
                    Name = OAuthUser.Name.Split(' ').FirstOrDefault()?.Trim() ?? OAuthUser.Name.Trim(),
                    OAuthProviderId = OAuthProvider.Id,
                    EmailVerified = OAuthUser.EmailVerified
                };

                await userRepository.InsertAsync(user);

                if (request.UserType == UserTypeEnum.Volunteer.GetDescription())
                {
                    var nameParts = OAuthUser.Name.Split(' ');

                    var volunteer = new Volunteer
                    {
                        UserId = user.Id,
                        Surname = string.Join(' ', nameParts.Skip(1)).Trim(),
                        BirthDate = default
                    };

                    await volunteerRepository.InsertAsync(volunteer);
                }
                else if (request.UserType == UserTypeEnum.Ngo.GetDescription())
                {
                }
                else if (request.UserType == UserTypeEnum.Company.GetDescription())
                {
                }
            }
            else if (!user.OAuthProviderId.HasValue)
            {
                user.OAuthProviderId = OAuthProvider.Id;

                await userRepository.UpdateAsync(user);
            }
            else if (user.OAuthProviderId != OAuthProvider.Id)
            {
                NotifyError("Já existe um usuário com o email informado vinculado a outro provedor.");
                return null;
            }

            if (!HasNotification() && await unitOfWork.CommitAsync())
            {
                request.ExecutedSuccessfullyCommand = true;

                if (!user.EmailVerified)
                {
                    var emailActivationToken = claimsService.GenerateGenericToken(user);

                    await mediator.PublishEvent(new EmailActivationEvent
                    {
                        EmailActivationToken = emailActivationToken,
                        User = user
                    });

                    NotifyError(Values.Message.EmailNotVerifiedError);
                    return null;
                }

                return new AuthenticationDto
                {
                    AccessToken = claimsService.GenerateAuthToken(user)
                };
            }
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}
