using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Propiedad.Queries.GetAllPropiedadById
{
    /// <summary>
    /// Parámetros para filtrar las propiedades por id
    /// </summary>  
    public class GetPropiedadByIdQuery : IRequest<PropiedadDto>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Colocar el id por el cual quiere filtrar las propiedades")]
        public int Id { get; set; }
    }

    public class GetPropiedadByIdQueryHandler : IRequestHandler<GetPropiedadByIdQuery, PropiedadDto>
    {
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetPropiedadByIdQueryHandler(IPropiedadRepository propiedadRepository, IMapper mapper, IUserService userService)
        {
            _propiedadRepository = propiedadRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<PropiedadDto> Handle(GetPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var propiedad = await GetByIdViewModel(request.Id);
            if (propiedad == null) throw new Exception("Propiedad no encontrada");

            propiedad.AgenteNombreCompleto = await _userService.GetUserFullNameById(propiedad.AgenteId);

            return propiedad;
        }

        private async Task<PropiedadDto> GetByIdViewModel(int id)
        {
            var propiedadList = await _propiedadRepository.GetAllAsync();
            var propiedad = propiedadList.FirstOrDefault(f => f.Id == id);

            if (propiedad == null)
            {
                return null;
            }

            PropiedadDto propiedadDto = new()
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
            };

            return propiedadDto;
        }
    }
}