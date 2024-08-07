using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Agente.Commands.ChangeEmailConfirmedStatus;
using RealEstateApp.Core.Application.Features.Agente.Queries.GetAgenteById;
using RealEstateApp.Core.Application.Features.Agente.Queries.GetAllAgentes;
using RealEstateApp.Core.Application.Features.Propiedad.Queries.GetPropiedadById;
using RealEstateApp.Core.Application.ViewModels.Agentes;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de agentes")]
    public class AgenteController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de agentes",
          Description = "Obtiene la información de los agentes y cuantas propiedades tienen"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "DESARROLLADOR, ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AgenteViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var agentes = await Mediator.Send(new GetAllAgentesQuery());
                return Ok(agentes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Agente por id",
          Description = "Obtiene un agente utilizando el id como filtro"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "DESARROLLADOR, ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgenteViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var agente = await Mediator.Send(new GetAgenteByIdQuery { Id = id });
                return Ok(agente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}/propiedades")]
        [SwaggerOperation(
          Summary = "Propiedad por id del agente",
          Description = "Obtiene las propiedades de un agente utilizando el id como filtro "
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "DESARROLLADOR, ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PropiedadDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropiedadesByAgenteId(string id)
        {
            try
            {
                var propiedades = await Mediator.Send(new GetPropiedadesByAgenteIdQuery { AgenteId = id });
                if (propiedades == null || !propiedades.Any())
                {
                    return NotFound();
                }

                return Ok(propiedades);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("changestatus")]
        [SwaggerOperation(
          Summary = "Cambiar el estado de agente",
          Description = "Cambie el estado de un agente a activo/inactivo"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeEmailConfirmedStatusCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                if (!result) return BadRequest("No se pudo cambiar el estado.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
