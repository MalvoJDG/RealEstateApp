using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.TipoVenta.Commands.CreateTipoVenta;
using RealEstateApp.Core.Application.Features.TipoVenta.Commands.DeleteTipoVentaById;
using RealEstateApp.Core.Application.Features.TipoVenta.Commands.UpdateTipoVenta;
using RealEstateApp.Core.Application.Features.TipoVenta.Queries.GetAllTipoVentas;
using RealEstateApp.Core.Application.Features.TipoVenta.Queries.GetTipoVentaById;
using RealEstateApp.Core.Application.ViewModels.TipoVentas;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de tipos de ventas")]
    public class TipoVentaController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de tipos de ventas",
          Description = "Obtiene todo el listado de tipos de ventas creados en el sistema"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "DESARROLLADOR, ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoVentaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllTipoVentasQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Tipo de venta por id",
          Description = "Obtiene un tipo de venta utilizando el id como filtro"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "DESARROLLADOR, ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTipoVentaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetTipoVentaByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [SwaggerOperation(
          Summary = "Creación de tipo de venta",
          Description = "Recibe los parametros necesarios para crear un nuevo tipo de venta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateTipoVentaCommand command)
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
          Summary = "Modificar tipo de venta",
          Description = "Recibe los parametros necesarios para modificar un tipo de venta existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTipoVentaViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateTipoVentaCommand command)
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
          Summary = "Eliminar un tipo de venta",
          Description = "Recibe los parametros necesarios para eliminar un tipo de venta existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTipoVentaByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
