using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Controllers
{
    public class HomeCliente : Controller
    {
        private readonly IAgenteService22 _agenteService;
        private readonly IPropiedadService _propiedadService;

        public HomeCliente(IAgenteService22 agenteService, IPropiedadService propiedadService)
        {
            _agenteService = agenteService;
            _propiedadService = propiedadService;
        }

        public async Task<IActionResult> Index()
        {
            var propiedades = await _propiedadService.GetAllViewModel();
            return View(propiedades);
        }

        public async Task<IActionResult> Agentes()
        {
            var agentes = await _agenteService.GetAllViewModelWithInclude();
            return View(agentes);
        }
    }
}
