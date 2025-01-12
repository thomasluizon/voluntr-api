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
using Voluntr.Domain.Models;

namespace Voluntr.Domain.CommandHandlers
{
    public class AssignQuestCommandHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        IVolunteerRepository volunteerRepository,
        IProjectRepository projectRepository,
        IQuestAssignmentRepository questAssignmentRepository,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<AssignQuestCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(AssignQuestCommand request)
        {
            var userId = claimsService.GetCurrentUserId();

            var volunteer = await volunteerRepository.GetFirstByExpressionAsync(x => x.UserId == userId);

            if (volunteer == null)
            {
                NotifyError("O voluntário informado não foi encontrado");
                return null;
            }

            var project = await projectRepository.GetFirstByExpressionAsync(
                x => x.Id == request.ProjectId,
                x => x.Quests
            );

            if (project == null)
            {
                NotifyError("O projeto informado não foi encontrado");
                return null;
            }

            var quest = project.Quests.FirstOrDefault(x => x.Id == request.Id);

            if (quest == null)
            {
                NotifyError("A tarefa informada não foi encontrada");
                return null;
            }

            if (await questAssignmentRepository.ExistsByExpressionAsync(
                x => x.QuestId == quest.Id && x.Status != QuestAssignmentStatusEnum.Rejected.GetDescription()
            ))
            {
                NotifyError("Esta tarefa já esta atribuída à outro voluntáro");
                return null;
            }

            var questAssignment = new QuestAssignment
            {
                QuestId = request.Id,
                VolunteerId = volunteer.Id,
                QuestAssignmentStatusEnum = QuestAssignmentStatusEnum.Pending
            };

            await questAssignmentRepository.InsertAsync(questAssignment);

            if (!HasNotification() && await unitOfWork.CommitAsync())
                request.ExecutedSuccessfullyCommand = true;
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}
