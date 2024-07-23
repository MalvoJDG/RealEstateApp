using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoVentas;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class TipoVentaService : GenericService<TipoVentaViewModel, TipoVentaViewModel, TipoVenta>, ITipoVentaService
    {
        private readonly ITipoVentaRepository _tipoventaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TipoVentaService(ITipoVentaRepository tipoventaRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(tipoventaRepository, mapper)
        {
            _tipoventaRepository = tipoventaRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }
}
