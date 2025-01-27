using AutoMapper;
using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.CommandHandlers
{
    public class AddQuestCommandHandler(
        IMediatorHandler mediator,
        IProjectRepository projectRepository,
        IQuestRepository questRepository,
        IAddressRepository addressRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : MediatorResponseCommandHandler<AddQuestCommand, CommandResponseDto>(mediator)
    {
        public override async Task<CommandResponseDto> AfterValidation(AddQuestCommand request)
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

            if (project.Quests.FirstOrDefault(x => x.Name == request.Name) != null)
            {
                NotifyError("Já existe uma tarefa com o nome informado neste projeto");
                return null;
            }

            if (request.DueDate > project.DueDate)
            {
                NotifyError("O prazo final da tarefa não pode ser maior que o prazo final do projeto");
                return null;
            }

            var quest = new Quest
            {
                ProjectId = project.Id,
                Name = request.Name,
                Description = request.Description,
                Reward = request.Reward,
                DueDate = request.DueDate,
                IsRemote = request.IsRemote,
                MaxVolunteers = request.MaxVolunteers
            };

            var address = mapper.Map<Address>(request.Address);
            address.QuestId = quest.Id;

            await addressRepository.InsertAsync(address);
            await questRepository.InsertAsync(quest);

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
