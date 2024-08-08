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
           var elements = await _service.GetAllViewModel();

            return View(elements);
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

            return RedirectToRoute(new { controller = "Propiedad", action = "Index" });
        }

        public async Task<IActionResult> EditView(int id)
        {
            var element = await _service.GetByIdSaveViewModel(id);
            return View(element);
        }


        public async Task<IActionResult>  Edit (SaveTipoPropiedadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditView", model);
            }

            await _service.Update(model,model.Id);

            return RedirectToRoute(new { controller = "TipoDePropiedad", action = "Index" });
        }

        public async Task<IActionResult> DeleteView(int id) {

            var element = await _service.GetByIdSaveViewModel(id);

            return View(element);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToRoute(new { controller = "TipoDePropiedad", action = "Index" });

        }
    }
}

