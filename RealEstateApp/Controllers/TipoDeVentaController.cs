using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Application.ViewModels.TipoVentas;

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

            return RedirectToRoute(new { controller = "TipoDeVenta", action = "Index" });
        }

        public async Task<IActionResult> EditView(int id)
        {
            var element = await _service.GetByIdSaveViewModel(id);
            return View(element);
        }


        public async Task<IActionResult> Edit(SaveTipoVentaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditView", model);
            }

            await _service.Update(model, model.Id);

            return RedirectToRoute(new { controller = "TipoDeVenta", action = "Index" });
        }

        public async Task<IActionResult> DeleteView(int id)
        {

            var element = await _service.GetByIdSaveViewModel(id);

            return View(element);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToRoute(new { controller = "TipoDeVenta", action = "Index" });

        }
    }
}

