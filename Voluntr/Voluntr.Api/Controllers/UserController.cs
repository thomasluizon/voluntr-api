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
    public class UserController(
        IMediatorHandler mediator,
        IUserServiceApp userServiceApp
    ) : ApiController(mediator)
    {
        /// <summary>
        /// Pausa/Despausa a conta do usuário
        /// </summary>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPost("toggle-pause")]
        public async Task<IActionResult> TogglePause()
        {
            await userServiceApp.TogglePause();

            return Response();
        }

        /// <summary>
        /// Deleta a conta do usuário
        /// </summary>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            await userServiceApp.DeleteAccount();

            return Response();
        }

        /// <summary>
        /// Realiza o upload da foto do usuário
        /// </summary>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPost("picture")]
        public async Task<IActionResult> UploadPicture([FromForm] UploadPictureRequestViewModel viewModel)
        {
            await userServiceApp.UploadPicture(viewModel);

            return Response();
        }

        /// <summary>
        /// Realiza a exclusão da foto do usuário
        /// </summary>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpDelete("picture")]
        public async Task<IActionResult> DeletePicture()
        {
            await userServiceApp.DeletePicture();

            return Response();
        }
    }
}