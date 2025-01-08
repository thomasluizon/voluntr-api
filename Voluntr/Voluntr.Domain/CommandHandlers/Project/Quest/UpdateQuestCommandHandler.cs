using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.UnitOfWork;

namespace Voluntr.Domain.CommandHandlers
{
    public class UpdateQuestCommandHandler(
        IMediatorHandler mediator,
        IProjectRepository projectRepository,
        IQuestRepository questRepository,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<UpdateQuestCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(UpdateQuestCommand request)
        {
            var project = await projectRepository.GetFirstByExpressionAsync(
                x => x.Id == request.ProjectId,
                x => x.Quests
            );

            if (project == null)
            {
                NotifyError("O projeto informado não foi encontrado");
                return null;
            }

            if (project.Quests.FirstOrDefault(x => x.Name == request.Name && x.Id != request.Id) != null)
            {
                NotifyError("Já existe uma tarefa com o nome informado neste projeto");
                return null;
            }

            if (request.DueDate > project.DueDate)
            {
                NotifyError("O prazo final da tarefa não pode ser maior que o prazo final do projeto");
                return null;
            }

            var quest = project.Quests.FirstOrDefault(x => x.Id == request.Id);

            if (quest == null)
            {
                NotifyError("A tarefa informada não foi encontrada");
                return null;
            }

            quest.Name = request.Name;
            quest.Description = request.Description;
            quest.Reward = request.Reward;
            quest.DueDate = request.DueDate;

            await questRepository.UpdateAsync(quest);

            if (!HasNotification() && await unitOfWork.CommitAsync())
            {
                request.ExecutedSuccessfullyCommand = true;

                return new CommandResponseDto { Id = quest.Id };
            }
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}
