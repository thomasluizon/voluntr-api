using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.UnitOfWork;

namespace Voluntr.Domain.CommandHandlers
{
    public class UpdateProjectCommandHandler(
        IMediatorHandler mediator,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<UpdateProjectCommand, CommandResponseDto>(mediator)
    {
        public async override Task<CommandResponseDto> AfterValidation(UpdateProjectCommand request)
        {
            var project = await projectRepository.GetByIdAsync(request.Id);

            if (project == null)
            {
                NotifyError("O projeto informado não foi encontrado");
                return null;
            }

            if (await projectRepository.ExistsByExpressionAsync(x => x.Name == request.Name && x.Id != project.Id))
            {
                NotifyError("Já existe outro projeto com o novo nome informado");
                return null;
            }

            project.Name = request.Name;
            project.Description = request.Description;
            project.DueDate = request.DueDate;

            await projectRepository.UpdateAsync(project);

            if (!HasNotification() && await unitOfWork.CommitAsync())
            {
                request.ExecutedSuccessfullyCommand = true;

                return new CommandResponseDto { Id = project.Id };
            }
            else
                NotifyError(Values.Message.DefaultError);

            return null;
        }
    }
}
