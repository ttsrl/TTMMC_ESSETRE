using Microsoft.AspNetCore.Mvc;

namespace TTMMC.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
