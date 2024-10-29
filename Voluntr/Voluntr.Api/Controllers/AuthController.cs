using Microsoft.AspNetCore.Authentication;
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
        IAuthenticationServiceApp authenticationServiceApp,
        IConfiguration configuration
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
        /// Redireciona o usuário para o Azure AD B2C para autenticação
        /// </summary>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [HttpGet("login/google")]
        public IActionResult GoogleLogin()
        {
            var url = configuration.GetSection("Urls").GetValue<string>("VoluntrApi");

            if (!string.IsNullOrEmpty(url))
            {
                return Challenge(new AuthenticationProperties
                {
                    RedirectUri = $"{url}/auth/google-callback"
                },
                "AzureAdB2C");
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Callback após autenticação via Google (recebe o authorization code)
        /// </summary>
        [HttpPost("google-callback")]
        [ProducesResponseType(typeof(AuthenticationResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GoogleCallback([FromForm] string code, [FromForm] string state)
        {
            var response = await authenticationServiceApp.HandleGoogleCallback(code, state);

            return Response(response);
        }
    }
}
