using Microsoft.AspNetCore.Mvc;
using TTMMC.Models.ViewModels;

namespace TTMMC.Controllers
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
            else if (id == 2)
            {
                txt = "Errore imprevisto. Impossibile modificare il cliente";
            }
            else if(id == 3)
            {
                txt = "Errore imprevisto. Impossibile aggiungere lo stampo";
            }
            else if (id == 4)
            {
                txt = "Errore imprevisto. Impossibile modificare lo stampo";
            }
            else if (id == 5)
            {
                txt = "Errore imprevisto. Impossibile aggiungere il master";
            }
            else if (id == 6)
            {
                txt = "Errore imprevisto. Impossibile modificare il master";
            }
            else if (id == 7)
            {
                txt = "Errore imprevisto. Impossibile aggiungere il materiale";
            }
            else if (id == 8)
            {
                txt = "Errore imprevisto. Impossibile modificare il materiale";
            }
            else if (id == 9)
            {
                txt = "Errore imprevisto. Impossibile aggiungere la miscela";
            }
            else if (id == 10)
            {
                txt = "Errore imprevisto. Impossibile modificare la miscela";
            }
            else if (id == 11)
            {
                txt = "Errore imprevisto. Impossibile creare la lavorazione";
            }
            else if (id == 12)
            {
                txt = "Errore imprevisto. Impossibile modificare la lavorazione";
            }
            //------------------- errori pdf ------------------
            else if (id == 13)
            {
                txt = "Errore imprevisto. Impossibile creare la scheda dello stampo";
            }
            else if (id == 14)
            {
                txt = "Errore imprevisto. Impossibile creare la scheda del cliente";
            }
            else if (id == 15)
            {
                txt = "Errore imprevisto. Impossibile creare la scheda di lavorazione";
            }
            else if (id == 16)
            {
                txt = "Errore imprevisto. Impossibile creare la scheda di report";
            }
            return View(new ResultsModel { Text = txt });
        }
    }
}