using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ProjectController(
        IMediatorHandler mediator,
        IProjectServiceApp projectServiceApp
    ) : ApiController(mediator)
    {
        /// <summary>
        /// Realiza a criação de um projeto
        /// </summary>
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
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] string id)
        {
            if (ValidateStringToGuidParams(id))
            {
                await projectServiceApp.DeleteProject(id);
            }

            return Response();
        }

        #region Quests

        /// <summary>
        /// Realiza a criação de uma tarefa
        /// </summary>
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
        /// Realiza a exclusão de um projeto
        /// </summary>
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

        #endregion
    }
}
