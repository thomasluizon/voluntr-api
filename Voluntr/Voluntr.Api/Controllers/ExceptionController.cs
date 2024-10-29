using Microsoft.AspNetCore.Mvc;
using Voluntr.Crosscutting.Domain.Controller;
using Voluntr.Crosscutting.Domain.MediatR;

namespace Voluntr.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ExceptionController(
        IMediatorHandler mediator
    ) : ApiController(mediator)
    {
        /// <summary>
        /// Sobe uma exception de teste
        /// </summary>
        /// <exception cref="Exception">Exception de teste</exception>
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status500InternalServerError)]
        [HttpGet("exception")]
        public Task<IActionResult> Exception()
        {
            throw new Exception("Teste exception");
        }
    }
}
