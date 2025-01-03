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
    public class VolunteerController(
        IMediatorHandler mediator,
        IVolunteerServiceApp volunteerServiceApp
    ) : ApiController(mediator)
    {
        /// <summary>
        /// Realiza a consulta do progresso do onboarding do voluntário
        /// </summary>
        [ProducesResponseType(typeof(List<OnboardingTaskViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpGet("onboarding")]
        public async Task<IActionResult> GetOnboarding()
        {
            var response = await volunteerServiceApp.GetOnboarding();

            return Response(response);
        }

        /// <summary>
        /// Realiza a consulta dos dados do perfil do voluntário
        /// </summary>
        [ProducesResponseType(typeof(VolunteerProfileViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var response = await volunteerServiceApp.GetProfile();

            return Response(response);
        }
    }
}
