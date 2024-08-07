using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.CreateTipoPropiedad;
using RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.DeleteTipoPropiedadById;
using RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.UpdateTipoPropiedad;
using RealEstateApp.Core.Application.Features.TipoPropiedad.Queries.GetAllTipoPropiedades;
using RealEstateApp.Core.Application.Features.TipoPropiedad.Queries.GetTipoPropiedadById;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Application.ViewModels.TipoVentas;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de tipos de propiedades")]
    public class TipoPropiedadController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de tipos de propiedades",
          Description = "Obtiene todo el listado de tipos de propiedades creados en el sistema"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "DESARROLLADOR, ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoPropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllTipoPropiedadesQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Tipo de propiedad por id",
          Description = "Obtiene un tipo de propiedad utilizando el id como filtro"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "DESARROLLADOR, ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTipoPropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetTipoPropiedadByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [SwaggerOperation(
          Summary = "Creación de tipo de propiedad",
          Description = "Recibe los parametros necesarios para crear un nuevo tipo de propiedad"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateTipoPropiedadCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
          Summary = "Modificar tipo de propiedad",
          Description = "Recibe los parametros necesarios para modificar un tipo de propiedad existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTipoVentaViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateTipoPropiedadCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.Id)
                {
                    return BadRequest();
                }
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
          Summary = "Eliminar un tipo de propiedad",
          Description = "Recibe los parametros necesarios para eliminar un tipo de propiedad existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTipoPropiedadByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
