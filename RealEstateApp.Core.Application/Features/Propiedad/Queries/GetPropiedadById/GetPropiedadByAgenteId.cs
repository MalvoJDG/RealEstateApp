using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Propiedad.Queries.GetPropiedadById
{
    public class GetPropiedadesByAgenteIdQuery : IRequest<IEnumerable<PropiedadDto>>
    {
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
