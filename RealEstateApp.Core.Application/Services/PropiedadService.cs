using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.ViewModels.Agentes;
namespace RealEstateApp.Core.Application.Services
{
    public class PropiedadService : GenericService<SavePropiedadViewModel, PropiedadViewModel, Propiedad>, IPropiedadService
    {
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AgenteViewModel _user;

        public PropiedadService(IPropiedadRepository propiedadRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(propiedadRepository, mapper)
        {
            _propiedadRepository = propiedadRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _user = httpContextAccessor.HttpContext.Session.Get<AgenteViewModel>("user");
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

        public override Task<SavePropiedadViewModel> Add(SavePropiedadViewModel vm)
        {
            var code = Guid.NewGuid().ToString();
            vm.Codigo = code.Substring(7);
            vm.AgenteId = _user.Id;
            vm.AgenteNombreCompleto = _user.FirstName + " " + _user.LastName;
            return base.Add(vm);
        }
    }
}
