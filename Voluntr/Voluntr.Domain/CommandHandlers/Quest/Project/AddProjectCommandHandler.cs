﻿using Voluntr.Crosscutting.Domain.Commands.Handlers;
using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Models;

namespace Voluntr.Domain.CommandHandlers
{
    public class AddProjectCommandHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        INgoRepository ngoRepository,
        IProjectRepository projectRepository
    ) : MediatorResponseCommandHandler<AddProjectCommand, CommandResponseDto>(mediator)
    {
        public async override Task<CommandResponseDto> AfterValidation(AddProjectCommand request)
        {
            if (claimsService.GetCurrentUserType() != UserTypeEnum.Ngo.GetDescription())
            {
                NotifyError("O usuário informado não é uma ONG");
                return null;
            }

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
        }
    }
}
