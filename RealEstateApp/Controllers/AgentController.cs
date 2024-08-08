using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.ViewModels.Users;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Middelwares;

namespace RealEstateApp.Controllers
{
    public class AgentController : Controller
    { 

        private readonly SaveUserViewModel _user;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPropiedadService _pservice;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IUserService _service;


        public AgentController(IHttpContextAccessor httpContextAccessor, IPropiedadService propiedadService, ValidateUserSession _validateUserSession) {

            _user = httpContextAccessor.HttpContext.Session.Get<SaveUserViewModel>("user");
            _pservice = propiedadService;
            this._validateUserSession = _validateUserSession;
            }

        public async Task<IActionResult> Index()
        {
            ViewBag.Propiedades = await _pservice.GetAllViewModel();

            return View();
        }

        public IActionResult Profile()
        {
            return View(_user);
        }

        public IActionResult EditView ()
        {
            var agent = _user;
            return View(agent);
        }

        public async Task<IActionResult> Edit(SaveUserViewModel model)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("EditView", model);
            }

            await _service.UpdateUser(model);

            return RedirectToRoute(new { controller = "TipoDePropiedad", action = "Index" });
        }
    }
}

