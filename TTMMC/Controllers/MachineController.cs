using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTMMC_ESSETRE.Models;
using TTMMC_ESSETRE.Models.ViewModels;
using TTMMC_ESSETRE.Services;

namespace TTMMC_ESSETRE.Controllers
{
    public class MachineController : Controller
    {
        private readonly MachinesService _machines;

        public MachineController([FromServices] MachinesService machines)
        {
            _machines = machines;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Details", new { id = 1 });
            //var machines = _machines.GetMachines().ToList();
            //var m = new IndexMachineModel
            //{
            //    Machines = machines
            //};
            //return View(m);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id != 0)
            {
                var mdb = _machines.GetMachineById(id);
                if (mdb is IMachine)
                {
                    var m = new MachineDetailsModel
                    {
                        Machine = mdb
                    };
                    return View(m);
                }

            }
            return RedirectToAction("Index");
        }

    }
}