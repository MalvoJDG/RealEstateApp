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

            return RedirectToRoute(new { controller = "Agent", action = "Index" });
        }
    }
}

