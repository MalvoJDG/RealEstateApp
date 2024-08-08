using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Propiedad.Queries.GetPropiedadById
{
    /// <summary>
    /// Parámetros para filtrar las propiedades por id del agente
    /// </summary>  
    public class GetPropiedadesByAgenteIdQuery : IRequest<IEnumerable<PropiedadDto>>
    {
        /// <example>f136c302-bf1d-44a1-8555-7d8ae497cce2</example>
        [SwaggerParameter(Description = "Colocar el AgenteId por la cual quiere filtrar las propiedades")]
        public string AgenteId { get; set; }
    }

    public class GetPropiedadesByAgenteIdQueryHandler : IRequestHandler<GetPropiedadesByAgenteIdQuery, IEnumerable<PropiedadDto>>
    {
        private readonly IPropiedadService _propiedadService;

        public GetPropiedadesByAgenteIdQueryHandler(IPropiedadService propiedadService)
        {
            _propiedadService = propiedadService;
        }

        public async Task<IEnumerable<PropiedadDto>> Handle(GetPropiedadesByAgenteIdQuery request, CancellationToken cancellationToken)
        {
            var propiedades = await _propiedadService.GetPropiedadesByAgenteId(request.AgenteId);
            if (propiedades == null || !propiedades.Any()) throw new Exception("Propiedades no encontradas");

            return propiedades;
        }
    }
}
