using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agente.Commands.ChangeEmailConfirmedStatus
{
    /// <summary>
    /// Parámetros para la cambiar el estado del agente
    /// </summary>  
    public class ChangeEmailConfirmedStatusCommand : IRequest<bool>
    {
        /// <example>f136c302-bf1d-44a1-8555-7d8ae497cce2</example>
        [SwaggerParameter(Description = "El id del agente")]
        public string Id { get; set; }

        /// <example>true</example>
        [SwaggerParameter(Description = "Activar o desactivar al agente")]
        public bool IsConfirmed { get; set; }
    }
    public class ChangeEmailConfirmedStatusCommandHandler : IRequestHandler<ChangeEmailConfirmedStatusCommand, bool>
    {
        private readonly IAgenteService _agenteService;

        public ChangeEmailConfirmedStatusCommandHandler(IAgenteService agenteService)
        {
            _agenteService = agenteService;
        }

        public async Task<bool> Handle(ChangeEmailConfirmedStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await _agenteService.ChangeEmailConfirmedStatusAsync(request.Id, request.IsConfirmed);
            if (!result) throw new Exception("No se pudo cambiar el estado. Asegúrese de que el ID corresponde a un agente.");

            return result;
        }
    }
}
