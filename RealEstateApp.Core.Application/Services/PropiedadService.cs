using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.ViewModels.Agentes;
using RealEstateApp.Core.Application.Dtos.Account;

namespace RealEstateApp.Core.Application.Services
{
    public class PropiedadService : GenericService<SavePropiedadViewModel, PropiedadViewModel, Propiedad>, IPropiedadService
    {
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoritoRepository _favoriteRepository;
        private readonly IAgenteService22 _agenteService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly AuthenticationResponse userViewModel;
        private readonly AgenteViewModel _user;

        public PropiedadService(IPropiedadRepository propiedadRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IFavoritoRepository favoritoRepository, IAccountService accountService) : base(propiedadRepository, mapper)
        {
            _propiedadRepository = propiedadRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _user = httpContextAccessor.HttpContext.Session.Get<AgenteViewModel>("user");
            userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _favoriteRepository = favoritoRepository;
            _accountService = accountService;
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
            var code = GenerateShortCode(5);
            vm.Codigo = code;
            vm.AgenteId = _user.Id;
            vm.AgenteNombreCompleto = _user.FirstName + " " + _user.LastName;
            return base.Add(vm);
        }

        private string GenerateShortCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<List<PropiedadViewModel>> GetAllViewModelWithFilters(FilterPropiedadViewModel filters)
        {
            // Obtener la lista de propiedades con las inclusiones necesarias
            var propiedades = await _propiedadRepository.GetAllWithFavoritesAsync();
            var userId = userViewModel.Id;

            var listViewModels = propiedades.Select(propiedad =>
            {
                var viewModel = _mapper.Map<PropiedadViewModel>(propiedad);
                if (userId != null)
                {
                    viewModel.EsFavorita = propiedad.Favorito.Any(f => f.User_Id == userId);
                }
                return viewModel;
            }).ToList();

            // Aplicar los filtros si existen
            if (!string.IsNullOrEmpty(filters.Tipo))
            {
                listViewModels = listViewModels.Where(propiedad => propiedad.Tipo.Equals(filters.Tipo, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (filters.PrecioMinimo.HasValue)
            {
                listViewModels = listViewModels.Where(propiedad => propiedad.Valor >= filters.PrecioMinimo.Value).ToList();
            }

            if (filters.PrecioMaximo.HasValue)
            {
                listViewModels = listViewModels.Where(propiedad => propiedad.Valor <= filters.PrecioMaximo.Value).ToList();
            }

            if (filters.CantidadHabitaciones.HasValue)
            {
                listViewModels = listViewModels.Where(propiedad => propiedad.CantidadHabitaciones == filters.CantidadHabitaciones.Value).ToList();
            }

            if (filters.CantidadBaños.HasValue)
            {
                listViewModels = listViewModels.Where(propiedad => propiedad.CantidadBaños == filters.CantidadBaños.Value).ToList();
            }

            return listViewModels;
        }


        public async Task<List<PropiedadViewModel>> GetAllFavoriteProperties(string userId)
        {

            var favoritos = await _favoriteRepository.GetAllAsync();
            var propiedadIds = favoritos
                .Where(favorito => favorito.User_Id == userId)
                .Select(favorito => favorito.Propiedad_Id)
                .ToList();

            var todasPropiedades = await _propiedadRepository.GetAllAsync();

            var propiedadesFavoritas = todasPropiedades
                .Where(propiedad => propiedadIds.Contains(propiedad.Id))
                .ToList();

            var result = propiedadesFavoritas.Select(propiedad =>
            {
                var viewModel = _mapper.Map<PropiedadViewModel>(propiedad);
                viewModel.EsFavorita = true; 
                return viewModel;
            }).ToList();

            return result;
        }

        public async Task<PropiedadViewModel> GetByIdViewModel(int id)
        {
            var propiedad = await _propiedadRepository.GetByIdAsync(id);
            var agente = await _accountService.GetUserByIdAsync(propiedad.AgenteId);
            var viewModel = _mapper.Map<PropiedadViewModel>(propiedad);

            viewModel.AgenteNombreCompleto = $"{agente.FirstName} {agente.LastName}";
            viewModel.Correo = agente.Email;
            viewModel.Telefono = agente.Phone;
            viewModel.Foto = agente.ProfilePictureUrl;

            

            return viewModel;
        }



    }
}

