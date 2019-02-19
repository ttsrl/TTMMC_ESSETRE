using Microsoft.AspNetCore.Mvc;
using TTMMC_ESSETRE.Models.ViewModels;

namespace TTMMC_ESSETRE.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int id)
        {
            var txt = "Errore imprevisto nel messaggio di report";
            if(id == 1)
            {
                txt = "Errore imprevisto. Impossibile aggiungere il cliente";
            }
            return View(new ResultsModel { Text = txt });
        }
    }
}