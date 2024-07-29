using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.TipoVentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoVenta.Queries.GetAllTipoVentas
{
    public class GetAllTipoVentasQuery : IRequest<IEnumerable<TipoVentaViewModel>>
    {
    }
    public class GetAllTipoVentasQueryHandler : IRequestHandler<GetAllTipoVentasQuery, IEnumerable<TipoVentaViewModel>>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;
        public GetAllTipoVentasQueryHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoVentaViewModel>> Handle(GetAllTipoVentasQuery request, CancellationToken cancellationToken)
        {
            var tipoVentasViewModel = await GetAllViewModelWithInclude();
            return tipoVentasViewModel;
        }

        private async Task<List<TipoVentaViewModel>> GetAllViewModelWithInclude()
        {
            var tipoVentaList = await _tipoVentaRepository.GetAllAsync();

            return tipoVentaList.Select(tipoVenta => new TipoVentaViewModel
            {
                Id = tipoVenta.Id,
                Nombre = tipoVenta.Nombre,
                Descripcion = tipoVenta.Descripcion
            }).ToList();
        }
    }
}