using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoPropiedad.Queries.GetTipoPropiedadById
{
    /// <summary>
    /// Parámetros para buscar un tipo de propiedad por id
    /// </summary>  
    public class GetTipoPropiedadByIdQuery : IRequest<TipoPropiedadViewModel>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "El id del tipo de propiedad")]
        public int Id { get; set; }
    }
    public class GetTipoPropiedadByIdQueryHandler : IRequestHandler<GetTipoPropiedadByIdQuery, TipoPropiedadViewModel>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;

        public GetTipoPropiedadByIdQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<TipoPropiedadViewModel> Handle(GetTipoPropiedadByIdQuery query, CancellationToken cancellationToken)
        {
            var tipoPropiedades = await _tipoPropiedadRepository.GetAllAsync();
            var tipoPropiedad = tipoPropiedades.FirstOrDefault(w => w.Id == query.Id);
            if (tipoPropiedad == null) throw new Exception($"TipoPropiedad no encontrada");
            var tipoPropiedadVm = _mapper.Map<TipoPropiedadViewModel>(tipoPropiedad);
            return tipoPropiedadVm;
        }
    }
}