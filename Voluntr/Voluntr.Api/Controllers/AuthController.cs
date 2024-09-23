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
    public class AuthController(
        IMediatorHandler mediator,
        IAuthenticationServiceApp authenticationServiceApp
    ) : ApiController(mediator)
    {

        /// <summary>
        /// Realiza o cadastro de um usuário
        /// </summary>
        /// <param name="viewModel">Dados do usuário</param>
        [ProducesResponseType(typeof(CommandResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel viewModel)
        {
            var response = await authenticationServiceApp.Register(viewModel);

            return Response(response);
        }
    }
}
