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
    //[Authorize]
    public class VolunteerController(
        IMediatorHandler mediator,
        IVolunteerServiceApp volunteerServiceApp
    ) : ApiController(mediator)
    {

        /// <summary>
        /// Realiza a consulta de todos os voluntários
        /// </summary>
        [ProducesResponseType(typeof(List<VolunteerResponseViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await volunteerServiceApp.GetVolunteers();

            return Response(response);
        }
    }
}
