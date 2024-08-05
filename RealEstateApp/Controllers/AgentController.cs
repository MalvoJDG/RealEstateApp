using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.Controllers
{
    public class AgentController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}

