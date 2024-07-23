using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class TipoPropiedadService : GenericService<SaveTipoPropiedadViewModel, TipoPropiedadViewModel, TipoPropiedad>, ITipoPropiedadService
    {
        private readonly ITipoPropiedadRepository _tipopropiedadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TipoPropiedadService(ITipoPropiedadRepository tipopropiedadRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(tipopropiedadRepository, mapper)
        {
            _tipopropiedadRepository = tipopropiedadRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }
}
