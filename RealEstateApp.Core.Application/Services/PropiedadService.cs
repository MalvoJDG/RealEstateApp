using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class PropiedadService : GenericService<SavePropiedadViewModel, PropiedadViewModel, Propiedad>, IPropiedadService
    {
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public PropiedadService(IPropiedadRepository propiedadRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(propiedadRepository, mapper)
        {
            _propiedadRepository = propiedadRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<int> GetPropiedadesCountByAgenteId(string agenteId)
        {
            return await _propiedadRepository.CountByAgenteIdAsync(agenteId);
        }

        public async Task<IEnumerable<PropiedadDto>> GetPropiedadesByAgenteId(string agenteId)
        {
            var propiedades = await _propiedadRepository.GetAllAsync();
            var propiedadesFiltradas = propiedades.Where(p => p.AgenteId == agenteId).ToList();

            return _mapper.Map<IEnumerable<PropiedadDto>>(propiedadesFiltradas);
        }
    }
}
