using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoPropiedad.Queries.GetAllTipoPropiedades
{
    /// <summary>
    /// Parámetros para el listado de tipos de propiedades
    /// </summary>  
    public class GetAllTipoPropiedadesQuery : IRequest<IEnumerable<TipoPropiedadViewModel>>
    {
    }
    public class GetAllTipoPropiedadesQueryHandler : IRequestHandler<GetAllTipoPropiedadesQuery, IEnumerable<TipoPropiedadViewModel>>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public GetAllTipoPropiedadesQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoPropiedadViewModel>> Handle(GetAllTipoPropiedadesQuery request, CancellationToken cancellationToken)
        {
            var tipoPropiedadesViewModel = await GetAllViewModelWithInclude();
            return tipoPropiedadesViewModel;
        }

        private async Task<List<TipoPropiedadViewModel>> GetAllViewModelWithInclude()
        {
            var tipoPropiedadList = await _tipoPropiedadRepository.GetAllAsync();

            return tipoPropiedadList.Select(tipoPropiedad => new TipoPropiedadViewModel
            {
                Id = tipoPropiedad.Id,
                Nombre = tipoPropiedad.Nombre,
                Descripcion = tipoPropiedad.Descripcion
            }).ToList();
        }
    }
}