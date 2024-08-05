using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agentes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class AgenteService : IAgenteService22
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;

        public AgenteService(IAccountService accountService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<AgenteViewModel>> GetAllViewModelWithInclude()
        {
            var users = await _accountService.GetAllUsersAsync(); // Assuming this method returns all users
            var agentes = users.Where(u => u.Roles.Contains("AGENTE")).ToList();

            var agentesViewModel = _mapper.Map<List<AgenteViewModel>>(agentes);
            return agentesViewModel;
        }
    }
}
