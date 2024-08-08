using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedades;

namespace RealEstateApp.Controllers
{
    public class HomeClienteController : Controller
    {
        private readonly IAgenteService22 _agenteService;
        private readonly IPropiedadService _propiedadService;
        private readonly AuthenticationResponse userViewModel;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoritoService _favoritoService;
        private readonly ITipoPropiedadService _tipoPropiedadService;

        public HomeClienteController(IAgenteService22 agenteService, IPropiedadService propiedadService, IHttpContextAccessor httpContextAccessor, IFavoritoService favoritoService, ITipoPropiedadService tipoPropiedadService)
        {
            _agenteService = agenteService;
            _propiedadService = propiedadService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _favoritoService = favoritoService;
            _tipoPropiedadService = tipoPropiedadService;
        }


        public async Task<IActionResult> Index(string tipo, decimal? precioMinimo, decimal? precioMaximo, int? cantidadHabitaciones, int? cantidadBaños, string Searchtearm)
        {
            var filters = new FilterPropiedadViewModel
            {
                Tipo = tipo,
                PrecioMinimo = precioMinimo,
                PrecioMaximo = precioMaximo,
                CantidadHabitaciones = cantidadHabitaciones,
                CantidadBaños = cantidadBaños,
                Searchtearm = Searchtearm 
            };

            ViewBag.Tipo = await _tipoPropiedadService.GetAllViewModel();
            var propiedades = await _propiedadService.GetAllViewModelWithFilters(filters);

            if (!string.IsNullOrWhiteSpace(filters.Searchtearm))
            {
                propiedades = propiedades.Where(s => s.Codigo.Contains(filters.Searchtearm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(propiedades);
        }


        public async Task<IActionResult> Favoritos()
        {
            var propiedades = await _propiedadService.GetAllFavoriteProperties(userViewModel.Id);
            return View(propiedades);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int propiedadId)
        {
            var userId = userViewModel.Id;
            var isFavorite = await _favoritoService.IsFavorite(userId, propiedadId);

            if (isFavorite)
            {
                await _favoritoService.RemoveFavorite(userId, propiedadId);
            }
            else
            {
                await _favoritoService.AddFavorite(userId, propiedadId);
            }
             
            return Ok();
        }

        public async Task<IActionResult> Agentes()
        {
            var agentes = await _agenteService.GetAllViewModelWithInclude();
            return View(agentes);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var propiedad = await _propiedadService.GetByIdViewModel(id);
            if (propiedad == null)
            {
                return NotFound();
            }
            return View(propiedad);
        }

    }
}
