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
    public class CauseController(
        IMediatorHandler mediator,
        ICauseServiceApp causeServiceApp
    ) : ApiController(mediator)
    {
        #region Achievements
        #endregion
    }
}
