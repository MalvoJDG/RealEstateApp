using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Mejoras;


namespace RealEstateApp.Controllers
{
    public class MejoraController : Controller
    {

        private readonly IMejoraService _service;

        public MejoraController(IMejoraService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Mejoras = await _service.GetAllViewModel();


            return View();
        }

        public IActionResult CreateView()
        {
            return View(new SaveMejoraViewModel());
        }

        public async Task<IActionResult> Create(SaveMejoraViewModel svm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateView", svm);
            }

            await _service.Add(svm);

            return RedirectToRoute(new { controller = "Mejora", action = "Index" });
        }

        public async Task<IActionResult> EditView(int id)
        {
            var element = await _service.GetByIdSaveViewModel(id);
            return View(element);
        }


        public async Task<IActionResult> Edit(SaveMejoraViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditView", model);
            }

            await _service.Update(model, model.Id);

            return RedirectToRoute(new { controller = "Mejora", action = "Index" });
        }
        public async Task<IActionResult> DeleteView(int id)
        {

            var element = await _service.GetByIdSaveViewModel(id);

            return View(element);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToRoute(new { controller = "Mejora", action = "Index" });

        }
    }
}

