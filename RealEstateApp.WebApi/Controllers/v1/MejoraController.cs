﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Mejora.Commands.CreateMejora;
using RealEstateApp.Core.Application.Features.Mejora.Commands.DeleteMejoraById;
using RealEstateApp.Core.Application.Features.Mejora.Commands.UpdateMejora;
using RealEstateApp.Core.Application.Features.Mejora.Queries.GetAllMejoras;
using RealEstateApp.Core.Application.Features.Mejora.Queries.GetMejoraById;
using RealEstateApp.Core.Application.ViewModels.Mejoras;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de mejoras")]
    public class MejoraController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de mejoras",
          Description = "Obtiene todo el listado de mejoras creado en el sistema"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "DESARROLLADOR, ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllMejorasQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Mejora por id",
          Description = "Obtiene una mejora utilizando el id como filtro"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "DESARROLLADOR, ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveMejoraViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetMejoraByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [SwaggerOperation(
          Summary = "Creación de mejora",
          Description = "Recibe los parametros necesarios para crear una nueva mejora"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateMejoraCommand command)
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
          Summary = "Modificar mejora",
          Description = "Recibe los parametros necesarios para modificar una mejora existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveMejoraViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateMejoraCommand command)
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
          Summary = "Eliminar una mejora",
          Description = "Recibe los parametros necesarios para eliminar una mejora existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteMejoraByIdCommand { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

