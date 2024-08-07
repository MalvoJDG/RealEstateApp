using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.ViewModels.Agentes;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.ViewModels.Users;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Controllers
{
    public class AgentController : Controller
    { 

        private readonly SaveUserViewModel _user;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPropiedadService _service;


        public AgentController(IHttpContextAccessor httpContextAccessor, IPropiedadService propiedadService) {

            _user = httpContextAccessor.HttpContext.Session.Get<SaveUserViewModel>("user");
            _service = propiedadService;
            }

        public async Task<IActionResult> Index()
        {
            ViewBag.Propiedades = await _service.GetAllViewModel();

            return View();
        }

        public IActionResult Profile()
        {
            return View(_user);
        }
    }
}

