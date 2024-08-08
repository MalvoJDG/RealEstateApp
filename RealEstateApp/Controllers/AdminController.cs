using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
