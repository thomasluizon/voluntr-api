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
        /// Realiza o cadastro do usuário
        /// </summary>
        /// <param name="viewModel">Dados do usuário</param>
        [ProducesResponseType(typeof(CommandResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel viewModel)
        {
            var response = await authenticationServiceApp.Register(viewModel);

            return Response(response);
        }

        /// <summary>
        /// Realiza o login do usuário
        /// </summary>
        /// <param name="viewModel">Dados do login</param>
        [ProducesResponseType(typeof(AuthenticationResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequestViewModel viewModel)
        {
            var response = await authenticationServiceApp.Login(viewModel);

            return Response(response);
        }
    }
}
