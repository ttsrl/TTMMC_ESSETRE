using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TTMMC.Models.ViewModels;

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

        public IActionResult Error(int id = 500)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Code = id });
        }

    }
}
