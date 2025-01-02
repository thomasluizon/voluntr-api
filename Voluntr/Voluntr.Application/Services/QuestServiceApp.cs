using AutoMapper;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;

namespace Voluntr.Application.Services
{
    public class QuestServiceApp(
        IMediatorHandler mediator,
        IMapper mapper
    ) : IProjectServiceApp
    {
        public async Task<CommandResponseViewModel> CreateProject(ProjectRequestViewModel viewModel, bool update = false)
        {
            if (!update)
            {
                var command = mapper.Map<AddProjectCommand>(viewModel);
                var response = await mediator.SendCommandResponse(command);

                return mapper.Map<CommandResponseViewModel>(response);
            }
            else
            {
                var command = mapper.Map<UpdateProjectCommand>(viewModel);
                var response = await mediator.SendCommandResponse(command);

                return mapper.Map<CommandResponseViewModel>(response);
            }
        }

        public async Task DeleteProject(string id)
        {
            var command = new DeleteProjectCommand { Id = Guid.Parse(id) };

            await mediator.SendCommandResponse(command);
        }

        public async Task<CommandResponseViewModel> CreateQuest(QuestRequestViewModel viewModel, string projectId, bool update = false)
        {
            viewModel.ProjectId = Guid.Parse(projectId);

            if (!update)
            {
                var command = mapper.Map<AddQuestCommand>(viewModel);
                var response = await mediator.SendCommandResponse(command);

                return mapper.Map<CommandResponseViewModel>(response);
            }
            else
            {
                var command = mapper.Map<UpdateQuestCommand>(viewModel);
                var response = await mediator.SendCommandResponse(command);

                return mapper.Map<CommandResponseViewModel>(response);
            }
        }

        public async Task DeleteQuest(string projectId, string questId)
        {
            var command = new DeleteQuestCommand
            {
                Id = Guid.Parse(questId),
                ProjectId = Guid.Parse(projectId)
            };

            await mediator.SendCommandResponse(command);
        }
    }
}
