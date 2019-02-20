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
                txt = "Errore imprevisto. Impossibile aggiungere la ricetta";
            }
            else if (id == 2)
            {
                txt = "Errore imprevisto. Impossibile modificare la ricetta";
            }
            else if (id == 3)
            {
                txt = "Errore imprevisto. Impossibile cancellare la ricetta";
            }
            else if (id == 4)
            {
                txt = "Errore imprevisto. Impossibile creare il pdf della ricetta";
            }
            return View(new ResultsModel { Text = txt });
        }
    }
}