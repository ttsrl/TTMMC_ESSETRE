using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTMMC.Models.ViewModels;

namespace TTMMC.Controllers
{
    public class MouldController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewMouldModel model)
        {
            return RedirectToAction("Index");
        }
    }
}