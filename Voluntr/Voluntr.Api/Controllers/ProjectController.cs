﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voluntr.Api.Attributes;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.Controller;
using Voluntr.Crosscutting.Domain.MediatR;

namespace Voluntr.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    [Ngo]
    public class ProjectController(
        IMediatorHandler mediator,
        IProjectServiceApp projectServiceApp
    ) : ApiController(mediator)
    {
        /// <summary>
        /// Realiza a criação de um projeto
        /// </summary>
        /// <param name="viewModel">Dados do projeto</param>
        [ProducesResponseType(typeof(CommandResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequestViewModel viewModel)
        {
            var response = await projectServiceApp.CreateProject(viewModel);

            return Response(response);
        }

        /// <summary>
        /// Realiza a atualização de um projeto
        /// </summary>
        /// <param name="viewModel">Dados do projeto</param>
        [ProducesResponseType(typeof(CommandResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectRequestViewModel viewModel)
        {
            var response = await projectServiceApp.CreateProject(viewModel, update: true);

            return Response(response);
        }

        /// <summary>
        /// Realiza a exclusão de um projeto
        /// </summary>
        /// <param name="projectId">Código do projeto</param>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject([FromRoute] string projectId)
        {
            if (ValidateStringToGuidParams(projectId))
            {
                await projectServiceApp.DeleteProject(projectId);
            }

            return Response();
        }

        #region Quests

        /// <summary>
        /// Realiza a criação de uma tarefa
        /// </summary>
        /// <param name="viewModel">Dados da tarefa</param>
        /// <param name="projectId">Código do projeto</param>
        [ProducesResponseType(typeof(CommandResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPost("{projectId}/quest")]
        public async Task<IActionResult> CreateQuest([FromBody] QuestRequestViewModel viewModel, string projectId)
        {
            if (ValidateStringToGuidParams(projectId))
            {
                var response = await projectServiceApp.CreateQuest(viewModel, projectId);

                return Response(response);
            }

            return Response();
        }

        /// <summary>
        /// Realiza a atualização de uma tarefa
        /// </summary>
        /// <param name="viewModel">Dados da tarefa</param>
        /// <param name="projectId">Código do projeto</param>
        [ProducesResponseType(typeof(CommandResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPut("{projectId}/quest")]
        public async Task<IActionResult> UpdateQuest([FromBody] QuestRequestViewModel viewModel, string projectId)
        {
            if (ValidateStringToGuidParams(projectId))
            {
                var response = await projectServiceApp.CreateQuest(viewModel, projectId, update: true);

                return Response(response);
            }

            return Response();
        }

        /// <summary>
        /// Realiza a exclusão de uma tarefa
        /// </summary>
        /// <param name="projectId">Código do projeto</param>
        /// <param name="questId">Código da tarefa</param>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpDelete("{projectId}/quest/{questId}")]
        public async Task<IActionResult> DeleteQuest(
            [FromRoute] string projectId,
            [FromRoute] string questId
        )
        {
            if (ValidateStringToGuidParams(projectId) && ValidateStringToGuidParams(questId))
            {
                await projectServiceApp.DeleteQuest(projectId, questId);
            }

            return Response();
        }


        /// <summary>
        /// Realiza a atribuição da tarefa ao voluntário
        /// </summary>
        /// <param name="projectId">Código do projeto</param>
        /// <param name="questId">Código da tarefa</param>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [Volunteer]
        [HttpPost("{projectId}/quest/{questId}/assign")]
        public async Task<IActionResult> AssignQuest(
            [FromRoute] string projectId,
            [FromRoute] string questId
        )
        {
            if (ValidateStringToGuidParams(projectId) && ValidateStringToGuidParams(questId))
            {
                await projectServiceApp.AssignQuest(projectId, questId);
            }

            return Response();
        }

        /// <summary>
        /// Realiza a submissão da tarefa por parte do voluntário
        /// </summary>
        /// <param name="projectId">Código do projeto</param>
        /// <param name="questId">Código da tarefa</param>
        /// <param name="viewModel">Dados da submissão</param>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [Volunteer]
        [HttpPost("{projectId}/quest/{questId}/submit")]
        public async Task<IActionResult> SubmitQuest(
            [FromRoute] string projectId,
            [FromRoute] string questId,
            [FromBody] SubmitQuestViewModel viewModel
        )
        {
            if (ValidateStringToGuidParams(projectId) && ValidateStringToGuidParams(questId))
            {
                await projectServiceApp.SubmitQuest(projectId, questId, viewModel);
            }

            return Response();
        }

        #endregion
    }
}
