using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Controllers
{
    public class HomeClienteController : Controller
    {
        private readonly IAgenteService22 _agenteService;
        private readonly IPropiedadService _propiedadService;
        private readonly AuthenticationResponse userViewModel;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoritoService _favoritoService;

        public HomeClienteController(IAgenteService22 agenteService, IPropiedadService propiedadService, IHttpContextAccessor httpContextAccessor, IFavoritoService favoritoService)
        {
            _agenteService = agenteService;
            _propiedadService = propiedadService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _favoritoService = favoritoService;
        }

        public async Task<IActionResult> Index()
        {
            var propiedades = await _propiedadService.GetAllViewModel();
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
