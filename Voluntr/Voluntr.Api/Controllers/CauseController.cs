﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voluntr.Api.Attributes;
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
    [Volunteer]
    public class CauseController(
        IMediatorHandler mediator,
        ICauseServiceApp causeServiceApp
    ) : ApiController(mediator)
    {
        #region Achievements

        /// <summary>
        /// Consulta as conquistas gerais e as causas para a página de conquistas
        /// </summary>
        [HttpGet("achievements")]
        [ProducesResponseType(typeof(AchievementsPageViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAchievementsPage()
        {
            var response = await causeServiceApp.GetAchievementsPage();

            return Response(response);
        }

        /// <summary>
        /// Consulta as conquistas de uma determinada causa
        /// </summary>
        [HttpGet("{id}/achievements")]
        [ProducesResponseType(typeof(CauseAchievementsPageViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCauseAchievementsPage(string id)
        {
            if (ValidateStringToGuidParams(id))
            {
                var response = await causeServiceApp.GetCauseAchievementsPage(id);

                return Response(response);
            }

            return Response();
        }

        #endregion
    }
}
