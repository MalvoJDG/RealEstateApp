using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Controllers
{
    public class HomeCliente : Controller
    {
        private readonly IAgenteService22 _agenteService;

        public HomeCliente(IAgenteService22 agenteService)
        {
            _agenteService = agenteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Agentes()
        {
            var agentes = await _agenteService.GetAllViewModelWithInclude();
            return View(agentes);
        }
    }
}
