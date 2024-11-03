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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequestViewModel viewModel)
        {
            var response = await authenticationServiceApp.Login(viewModel);

            return Response(response);
        }

        /// <summary>
        /// Realiza o login do usuário pelo Google
        /// </summary>
        /// <param name="viewModel">Dados do login pelo Google</param>
        [ProducesResponseType(typeof(AuthenticationResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [HttpPost("login/google")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleAuthenticationRequestViewModel viewModel)
        {
            var response = await authenticationServiceApp.LoginWithGoogle(viewModel);

            return Ok(response);
        }
    }
}
