using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Propiedad.Queries.GetAllPropiedadById;
using RealEstateApp.Core.Application.Features.Propiedad.Queries.GetAllPropiedades;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "DESARROLLADOR, ADMIN")]
    [SwaggerTag("Mantenimiento de propiedades")]
    public class PropiedadController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de propiedades",
          Description = "Muestra un listado de todas las propiedades en el sistema"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllPropiedadesQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Propiedad por id",
          Description = "Muestra una propiedad utilizando el id como filtro"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetPropiedadByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("codigo/{codigo}")]
        [SwaggerOperation(
          Summary = "Propiedad por código",
          Description = "Muestra una propiedad utilizando el código como filtro"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            try
            {
                var query = new GetAllPropiedadesQuery { Codigo = codigo };
                var propiedades = await Mediator.Send(query);

                if (propiedades == null || propiedades.Count == 0)
                {
                    return NotFound();
                }

                return Ok(propiedades.First());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}