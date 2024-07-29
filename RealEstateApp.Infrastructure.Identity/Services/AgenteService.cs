using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agentes;
using RealEstateApp.Infraestructure.Identity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class AgenteService : IAgenteService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPropiedadService _propiedadService;
        private readonly IMapper _mapper;

        public AgenteService(UserManager<ApplicationUser> userManager, IPropiedadService propiedadService, IMapper mapper)
        {
            _userManager = userManager;
            _propiedadService = propiedadService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AgenteViewModel>> GetAgentesAsync()
        {
            var usuarios = await _userManager.GetUsersInRoleAsync("AGENTE");

            var apiAgentes = usuarios.Select(u => new AgenteApi
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            });

            var agentes = apiAgentes.Select(async a =>
            {
                var agenteVm = _mapper.Map<AgenteViewModel>(a);
                agenteVm.CantidadPropiedades = await _propiedadService.GetPropiedadesCountByAgenteId(a.Id);
                return agenteVm;
            });

            return await Task.WhenAll(agentes);
        }

        public async Task<AgenteViewModel> GetAgenteByIdAsync(string id)
        {
            var usuarios = await _userManager.GetUsersInRoleAsync("AGENTE");
            var agente = usuarios.FirstOrDefault(a => a.Id == id);

            if (agente == null) return null;

            var apiAgente = new AgenteApi
            {
                Id = agente.Id,
                FirstName = agente.FirstName,
                LastName = agente.LastName,
                Email = agente.Email,
                PhoneNumber = agente.PhoneNumber
            };

            var agenteVm = _mapper.Map<AgenteViewModel>(apiAgente);
            agenteVm.CantidadPropiedades = await _propiedadService.GetPropiedadesCountByAgenteId(id);
            return agenteVm;
        }

        public async Task<bool> ChangeEmailConfirmedStatusAsync(string id, bool isConfirmed)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("AGENTE")) return false;

            user.EmailConfirmed = isConfirmed;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}