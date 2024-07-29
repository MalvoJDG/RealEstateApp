using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.TipoVentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoVenta.Queries.GetTipoVentaById
{
    public class GetTipoVentaByIdQuery : IRequest<TipoVentaViewModel>
    {
        public int Id { get; set; }
    }
    public class GetTipoVentaByIdQueryHandler : IRequestHandler<GetTipoVentaByIdQuery, TipoVentaViewModel>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;

        public GetTipoVentaByIdQueryHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<TipoVentaViewModel> Handle(GetTipoVentaByIdQuery query, CancellationToken cancellationToken)
        {
            var tipoVentas = await _tipoVentaRepository.GetAllAsync();
            var tipoVenta = tipoVentas.FirstOrDefault(w => w.Id == query.Id);
            if (tipoVenta == null) throw new Exception($"TipoVenta no encontrada");
            var tipoVentaVm = _mapper.Map<TipoVentaViewModel>(tipoVenta);
            return tipoVentaVm;
        }
    }
}