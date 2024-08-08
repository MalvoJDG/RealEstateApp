using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.ViewModels.Users;
using RealEstateApp.Core.Application.Interfaces.Services;

using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Middelwares;

using RealEstateApp.Core.Application.Dtos.Account;


namespace RealEstateApp.Controllers
{
    public class AgentController : Controller
    { 

        private readonly SaveUserViewModel _user;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IUserService _userService;
        private readonly IPropiedadService _service;
        private readonly AuthenticationResponse userViewModel;



        public AgentController(IHttpContextAccessor httpContextAccessor, IPropiedadService propiedadService, ValidateUserSession _validateUserSession, IUserService userService = null)
        {

            _user = httpContextAccessor.HttpContext.Session.Get<SaveUserViewModel>("user");
            userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _service = propiedadService;
            this._validateUserSession = _validateUserSession;
            _userService = userService;
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

            await _userService.UpdateUser(model);

            return RedirectToRoute(new { controller = "TipoDePropiedad", action = "Index" });
        }
    }
}

