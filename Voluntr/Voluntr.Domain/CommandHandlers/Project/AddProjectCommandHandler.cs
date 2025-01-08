using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Helpers.Constants;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Interfaces.UnitOfWork;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.CommandHandlers
{
    public class AddProjectCommandHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        INgoRepository ngoRepository,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
    ) : MediatorResponseCommandHandler<AddProjectCommand, CommandResponseDto>(mediator)
    {
        public async override Task<CommandResponseDto> AfterValidation(AddProjectCommand request)
        {
            var ngo = await ngoRepository.GetFirstByExpressionAsync(
                x => x.UserId == claimsService.GetCurrentUserId()
            );

            if (ngo == null)
            {
                NotifyError("A ONG informada não foi encontrada");
                return null;
            }

            if (await projectRepository.ExistsByExpressionAsync(x => x.Name == request.Name))
            {
                NotifyError("Já existe um projeto com o nome informado");
                return null;
            }

            var project = new Project
            {
                NgoId = ngo.Id,
                Name = request.Name,
                Description = request.Description,
                DueDate = request.DueDate
            };

            await projectRepository.InsertAsync(project);

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
