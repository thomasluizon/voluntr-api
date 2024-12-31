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
    ) : IQuestServiceApp
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
    }
}
