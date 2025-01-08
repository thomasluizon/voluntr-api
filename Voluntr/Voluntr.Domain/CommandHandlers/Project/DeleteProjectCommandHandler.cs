using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;

namespace Voluntr.Domain.CommandHandlers
{
    public class DeleteProjectCommandHandler(
        IMediatorHandler mediator,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<DeleteProjectCommand, CommandResponseDto>(mediator)
    {
        public async override Task<CommandResponseDto> AfterValidation(DeleteProjectCommand request)
        {
            if (!await projectRepository.ExistsByExpressionAsync(x => x.Id == request.Id))
            {
                NotifyError("O projeto informado não foi encontrado");
                return null;
            }

            await projectRepository.DeleteByIdAsync(request.Id);

            if (!HasNotification() && await unitOfWork.CommitAsync())
                request.ExecutedSuccessfullyCommand = true;
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}
