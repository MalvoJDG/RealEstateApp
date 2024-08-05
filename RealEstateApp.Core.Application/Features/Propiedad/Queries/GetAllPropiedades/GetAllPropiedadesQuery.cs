using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Propiedad.Queries.GetAllPropiedades
{
    /// <summary>
    /// Parámetros para filtrar las propiedades por codigo
    /// </summary>  
    public class GetAllPropiedadesQuery : IRequest<IList<PropiedadDto>>
    {
        /// <example>123456</example>
        [SwaggerParameter(Description = "Colocar el codigo de la propiedad por la cual quiere filtrar")]
        public string Codigo { get; set; }
    }

    public class GetAllPropiedadesQueryHandler : IRequestHandler<GetAllPropiedadesQuery, IList<PropiedadDto>>
    {
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetAllPropiedadesQueryHandler(IPropiedadRepository propiedadRepository, IMapper mapper, IUserService userService)
        {
            _propiedadRepository = propiedadRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IList<PropiedadDto>> Handle(GetAllPropiedadesQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllPropiedadesParameter>(request);
            var propiedadList = await GetAllViewModelWithFilters(filter);
            if (propiedadList == null || propiedadList.Count == 0) throw new Exception("Propiedades no encontradas");

            foreach (var propiedad in propiedadList)
            {
                propiedad.AgenteNombreCompleto = await _userService.GetUserFullNameById(propiedad.AgenteId);
            }

            return propiedadList;
        }

        private async Task<List<PropiedadDto>> GetAllViewModelWithFilters(GetAllPropiedadesParameter filters)
        {
            var propiedadList = await _propiedadRepository.GetAllAsync();

            var listViewModels = propiedadList.Select(propiedad => new PropiedadDto
            {
                Id = propiedad.Id,
                Codigo = propiedad.Codigo,
                Tipo = propiedad.Tipo,
                TipoVenta = propiedad.TipoVenta,
                Valor = propiedad.Valor,
                Tamaño = propiedad.Tamaño,
                CantidadHabitaciones = propiedad.CantidadHabitaciones,
                CantidadBaños = propiedad.CantidadBaños,
                Descripcion = propiedad.Descripcion,
                Mejoras = propiedad.Mejoras,
                AgenteId = propiedad.AgenteId
            }).ToList();

            if (!string.IsNullOrEmpty(filters.Codigo))
            {
                listViewModels = listViewModels.Where(propiedad => propiedad.Codigo == filters.Codigo).ToList();
            }

            return listViewModels;
        }
    }
}