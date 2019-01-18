using Microsoft.AspNetCore.Mvc;
using TTMMC.Models.ViewModels;

namespace TTMMC.Controllers
{
    public class ResultsController : Controller
    {
        public IActionResult Index(int id)
        {
            var txt = "Errore imprevisto nel messaggio di report";
            if(id == 1)
            {
                txt = "Errore imprevisto. Impossibile aggiungere lo stampo.";
            }
            else if (id == 2)
            {
                txt = "Errore imprevisto. Impossibile aggiungere il cliente.";
            }
            return View(new ResultsModel { Text = txt });
        }
    }
}