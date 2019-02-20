using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TTMMC_ESSETRE.Models.ViewModels;

namespace TTMMC_ESSETRE.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Machine");
        }

        public IActionResult Error(int id = 500)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Code = id });
        }

    }
}
