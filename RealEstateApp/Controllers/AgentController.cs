using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.ViewModels.Agentes;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.ViewModels.Users;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.Dtos.Account;

namespace RealEstateApp.Controllers
{
    public class AgentController : Controller
    { 

        private readonly SaveUserViewModel _user;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPropiedadService _service;
        private readonly AuthenticationResponse userViewModel;



        public AgentController(IHttpContextAccessor httpContextAccessor, IPropiedadService propiedadService) {

            _user = httpContextAccessor.HttpContext.Session.Get<SaveUserViewModel>("user");
            userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _service = propiedadService;
        }

        public async Task<IActionResult> Index()
        {
            var propiedades = await _service.GetAllByAgente(userViewModel.Id);
            return View(propiedades);
        }

        public IActionResult Profile()
        {
            return View(_user);
        }
    }
}

