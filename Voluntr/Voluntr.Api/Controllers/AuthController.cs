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
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequestViewModel viewModel)
        {
            var response = await authenticationServiceApp.Login(viewModel);

            return Response(response);
        }

        #region Reset Password

        /// <summary>
        /// Realiza uma requisição de redefinição de senha para o usuário não logado
        /// </summary>
        /// <param name="viewModel">Dados da requisição da redefinição de senha</param>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("reset-password-request")]
        public async Task<IActionResult> ResetPasswordRequest([FromBody] ResetPasswordRequestViewModel viewModel)
        {
            await authenticationServiceApp.ResetPasswordRequest(viewModel);

            return Response();
        }

        #endregion

        #region OAuth

        /// <summary>
        /// Realiza o login do usuário por OAuth
        /// </summary>
        /// <param name="viewModel">Dados do login por OAuth</param>
        [ProducesResponseType(typeof(AuthenticationResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        [HttpPost("oauth/login")]
        public async Task<IActionResult> OAuthLogin([FromBody] OAuthAuthenticationRequestViewModel viewModel)
        {
            var response = await authenticationServiceApp.OAuthLogin(viewModel);

            return Response(response);
        }

        /// <summary>
        /// Realiza a vinculação do usuário com um provedor de OAuth, ou desvincula caso já esteja vinculado
        /// </summary>
        /// <param name="OAuthProviderName">Provider do OAuth (Google)</param>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpPost("oauth/link/{OAuthProviderName}")]
        public async Task<IActionResult> LinkOAuth([FromRoute] string OAuthProviderName)
        {
            await authenticationServiceApp.LinkOAuth(OAuthProviderName);

            return Response();
        }

        #endregion
    }
}
