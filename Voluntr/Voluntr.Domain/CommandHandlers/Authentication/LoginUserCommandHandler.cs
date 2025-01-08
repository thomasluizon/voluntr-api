using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Events;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;

namespace Voluntr.Domain.CommandHandlers
{
    public class LoginUserCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        ICryptographyService cryptographyService,
        IClaimsService claimsService,
        IVolunteerRepository volunteerRepository,
        ICompanyRepository companyRepository,
        INgoRepository ngoRepository
    ) : MediatorResponseCommandHandler<LoginUserCommand, AuthenticationDto>(mediator)
    {
        public override async Task<AuthenticationDto> AfterValidation(LoginUserCommand request)
        {
            var user = await userRepository.GetFirstByExpressionAsync(x => x.Email == request.Email.Trim());

            if (
                user == null ||
                string.IsNullOrEmpty(user.Password) ||
                cryptographyService.Decrypt(user.Password) != request.Password.Trim()
            )
            {
                NotifyError("Credenciais inválidas");
                return null;
            }

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

            var userType = await GetUserType(user.Id);

            if (userType == null)
            {
                NotifyError(Values.Message.UserTypeNotFound);
                return null;
            }

            return new AuthenticationDto
            {
                AccessToken = claimsService.GenerateAuthToken(user, userType.Value),
            };
        }

        private async Task<UserTypeEnum?> GetUserType(Guid id)
        {
            if (await volunteerRepository.ExistsByExpressionAsync(x => x.UserId == id))
            {
                return UserTypeEnum.Volunteer;
            }
            else if (await ngoRepository.ExistsByExpressionAsync(x => x.UserId == id))
            {
                return UserTypeEnum.Ngo;
            }
            else if (await companyRepository.ExistsByExpressionAsync(x => x.UserId == id))
            {
                return UserTypeEnum.Company;
            }

            return null;
        }
    }
}
