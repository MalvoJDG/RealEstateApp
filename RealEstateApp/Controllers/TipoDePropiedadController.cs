using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Controllers
{
    public class TipoDePropiedadController : Controller
    {
        private readonly ITipoPropiedadService _service;

        public TipoDePropiedadController( ITipoPropiedadService service )
        {
            _service = service;
        }
      
        public async Task<IActionResult> Index()
        { 
            ViewBag.Tipos = await _service.GetAllViewModel();

            return View();
        }

        public IActionResult CreateView()
        {
            return View(new SaveTipoPropiedadViewModel());
        }

        public async Task<IActionResult> Create(SaveTipoPropiedadViewModel svm)
        {
            if(!ModelState.IsValid)
            {
                return View("CreateView", svm);
            }

            await _service.Add(svm);

            return RedirectToRoute(new { controller = "Agent", action = "Index" });
        }
    }
}

