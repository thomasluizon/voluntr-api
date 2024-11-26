using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voluntr.Application.Interfaces.Services;
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
        [HttpPost("toggle-user-pause")]
        public async Task<IActionResult> ToggleUserPause()
        {
            await userServiceApp.ToggleUserPause();

            return Response();
        }
    }
}
