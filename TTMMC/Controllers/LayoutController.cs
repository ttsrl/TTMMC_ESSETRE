using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMMC_ESSETRE.Models;
using TTMMC_ESSETRE.Models.DBModels;
using TTMMC_ESSETRE.Models.ViewModels;
using TTMMC_ESSETRE.Services;

namespace TTMMC_ESSETRE.Controllers
{
    public class LayoutController : Controller
    {
        private readonly MachinesService _machines;
        private readonly owlDBContext _owlDb;

        public LayoutController(MachinesService machines, owlDBContext owlDb)
        {
            _machines = machines ?? throw new ArgumentNullException(nameof(machines));
            _owlDb = owlDb ?? throw new ArgumentNullException(nameof(owlDb));
        }

        public async Task<IActionResult> Index()
        {
            var datiesterni = await _owlDb.Decofast35Datiesterni.ToListAsync();

            return View();
        }

        /*[HttpGet]
        public async Task<IActionResult> Start(int id)
        {
            if(id != 0)
            {
                var layout = await _dB.Layouts.Where(l => l.Status == Status.Waiting).FirstOrDefaultAsync(l => l.Id == id);
                if(layout is Layout)
                {
                    if(!_lListener.Contains(layout))
                        _lListener.Add(layout);
                    var ll = _lListener.GetLayoutListenItem(layout);
                    ll.TimerTick = 2;
                    await ll.Start();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Stop(int id)
        {
            if (id != 0)
            {
                var layout = await _dB.Layouts.Where(l => l.Status == Status.Recording).FirstOrDefaultAsync(l => l.Id == id);
                if (layout is Layout)
                {
                    await _lListener.Remove(layout);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Report(int id)
        {
            if(id != 0)
            {
                var layout = await _dB.Layouts.Where(l => l.Status == Status.Finished || l.Status == Status.Stopped).FirstOrDefaultAsync(l => l.Id == id);
                if(layout is Layout)
                {
                    return RedirectToAction("Report", "Pdf", new { id });
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewModule(int id)
        {
            if (id != 0)
            {
                var layout = await _dB.Layouts.FindAsync(id);
                if (layout is Layout)
                {
                    return RedirectToAction("LayoutModule", "Pdf", new { id });
                }
            }
            return RedirectToAction("Index");
        }*/

    }
}