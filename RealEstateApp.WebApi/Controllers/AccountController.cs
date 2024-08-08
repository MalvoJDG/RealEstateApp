using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Sistema de cuentas y membresia")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
            Summary = "Login de administrador y desarrollador",
            Description = "Autentica el usuario en el sistema y le retorna un JWT"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var result = await _accountService.ApiAuthenticationAsync(request);
            if (result.HasError)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }
        [HttpPost("register/desarrollador")]
        [SwaggerOperation(
            Summary = "Creacion de usuario desarrollador",
            Description = "Recibe los parametros necesarios para crear un usuario con el rol de desarrollador"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterDevAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterDevUserAsync(request, origin));
        }

        [HttpPost("register/admin")]
        [SwaggerOperation(
            Summary = "Creacion de usuario administrador",
            Description = "Recibe los parametros necesarios para crear un usuario con el rol de administrador"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAdminAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAdminUserAsync(request, origin));
        }

    }
}
