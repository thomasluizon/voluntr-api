using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler(
        IMediatorHandler mediator,
        IUserRepository userRepository,
        ICryptographyService cryptographyService,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<RegisterUserCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(RegisterUserCommand request)
        {
            var userExists = await userRepository.ExistsByExpressionAsync(
                x => x.Email == request.Email
            );

            if (userExists)
            {
                NotifyError("Já existe um usuário com o email informado");
                return null;
            }

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Password = cryptographyService.Encrypt(request.Password)
            };

            await userRepository.InsertAsync(user);

            if (!HasNotification() && await unitOfWork.CommitAsync())
            {
                request.ExecutedSuccessfullyCommand = true;

                return new CommandResponseDto { Id = user.Id };
            }
            else
                NotifyError(Values.Message.DefaultError);
        }
    }
}
