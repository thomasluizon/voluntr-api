﻿using Microsoft.AspNetCore.Mvc;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Application.ViewModels;
using Voluntr.Crosscutting.Domain.Controller;
using Voluntr.Crosscutting.Domain.MediatR;

namespace Voluntr.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ExternalController(
        IMediatorHandler mediator,
        IExternalServiceApp externalServiceApp
    ) : ApiController(mediator)
    {
        /// <summary>
        /// Retorna as informações do endereço do usuário baseado no CEP
        /// </summary>
        /// <param name="zipCode">CEP do usuário</param>
        [ProducesResponseType(typeof(ZipCodeInformationViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [HttpGet("zip-code")]
        public async Task<IActionResult> GetZipCodeInformation([FromQuery] string zipCode)
        {
            var response = await externalServiceApp.GetZipCodeInformation(zipCode);

            return Response(response);
        }
    }
}
