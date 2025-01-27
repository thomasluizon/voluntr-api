using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.Interfaces.Services;
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
    public class SubmitQuestCommandHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        IVolunteerRepository volunteerRepository,
        IProjectRepository projectRepository,
        IQuestAssignmentRepository questAssignmentRepository,
        IUnitOfWork unitOfWork,
        IStorageService storageService
    ) : MediatorResponseCommandHandler<SubmitQuestCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(SubmitQuestCommand request)
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

            var extension = Path.GetExtension(request.Image.FileName)?.ToLower();

            var container = Values.BlobPath.ImagesContainer;
            var path = string.Format(Values.BlobPath.QuestPictures, quest.Id);
            var fileName = $"submission{extension}";

            await storageService.DeleteAllFiles(container, path);

            using var memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var imageUrl = await storageService.UploadFile(container, path, fileName, fileBytes);

            var questAssignment = new QuestAssignment
            {
                QuestId = quest.Id,
                VolunteerId = volunteer.Id,
                QuestAssignmentStatusEnum = QuestAssignmentStatusEnum.Submitted,
                SubmissionDate = DateTime.Now.ToBrazilianTimezone(),
                Description = quest.Description,
                ImageAttachmentUrl = imageUrl
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
