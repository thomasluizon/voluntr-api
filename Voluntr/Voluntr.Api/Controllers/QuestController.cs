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
    public class QuestController(
        IMediatorHandler mediator,
        IQuestServiceApp questServiceApp
    ) : ApiController(mediator)
    {
        #region Projects

        /// <summary>
        /// Realiza a criação de um projeto
        /// </summary>
        [ProducesResponseType(typeof(CommandResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPost("project")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequestViewModel viewModel)
        {
            var response = await questServiceApp.CreateProject(viewModel);

            return Response(response);
        }

        /// <summary>
        /// Realiza a atualização de um projeto
        /// </summary>
        [ProducesResponseType(typeof(CommandResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPut("project")]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectRequestViewModel viewModel)
        {
            var response = await questServiceApp.CreateProject(viewModel, update: true);

            return Response(response);
        }

        /// <summary>
        /// Realiza a exclusão de um projeto
        /// </summary>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpDelete("project/{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] string id)
        {
            if (ValidateStringToGuidParams(id))
            {
                await questServiceApp.DeleteProject(id);
            }

            return Response();
        }

        #endregion
    }
}
