using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoVentas;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealEstateApp.Controllers
{
    public class TipoDeVentaController : Controller
    {

        private readonly ITipoVentaService _service;

        public TipoDeVentaController(ITipoVentaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TipoVentas = await _service.GetAllViewModel();

            return View();
        }

        public IActionResult CreateView()
        {
            return View(new SaveTipoVentaViewModel());
        }

        public async Task<IActionResult> Create(SaveTipoVentaViewModel svm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateView", svm);
            }

            await _service.Add(svm);

            return RedirectToRoute(new { controller = "Agent", action = "Index" });
        }
    }
}

