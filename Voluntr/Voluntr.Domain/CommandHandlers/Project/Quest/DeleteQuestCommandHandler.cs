using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.UnitOfWork;

namespace Voluntr.Domain.CommandHandlers
{
    public class DeleteQuestCommandHandler(
        IMediatorHandler mediator,
        IQuestRepository questRepository,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<DeleteQuestCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(DeleteQuestCommand request)
        {
            if (!await questRepository.ExistsByExpressionAsync(x => x.Id == request.Id))
            {
                NotifyError("A tarefa informado não foi encontrado");
                return null;
            }

            await questRepository.DeleteByIdAsync(request.Id);

            if (!HasNotification() && await unitOfWork.CommitAsync())
                request.ExecutedSuccessfullyCommand = true;
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}
