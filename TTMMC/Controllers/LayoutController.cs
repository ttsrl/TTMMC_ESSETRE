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
        private readonly TTMMCContext _dB;
        private readonly LayoutListener _lListener;

        public LayoutController(MachinesService machines, owlDBContext owlDb, TTMMCContext dB, LayoutListener lListener)
        {
            _machines = machines ?? throw new ArgumentNullException(nameof(machines));
            _owlDb = owlDb ?? throw new ArgumentNullException(nameof(owlDb));
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
            _lListener = lListener ?? throw new ArgumentNullException(nameof(lListener));
        }

        public async Task<IActionResult> Index()
        {
            var disposizioni = await _owlDb.Decofast35Datiesterni.ToListAsync();
            var layouts = await _dB.Layouts.ToListAsync();
            foreach (var d in disposizioni)
            {
                bool exist = false;
                foreach (var l in layouts)
                {
                    if (l.Color == d.Colore && l.ItemCode == d.CodiceArticolo && l.ItemDescription == d.DescrizioneArticolo && 
                        l.LayoutNumber == d.NumDisposizione && l.LayoutPhase == d.FaseDisposizione && l.LayoutType == d.TipoDisposizione &&
                        l.MachineName == d.NomeMacchina && l.MachineNumber == d.NumeroMacchina && l.Meters == d.MetriDisposti && 
                        l.Quantity == d.PezzeDisposte)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    int machine = (_machines.GetMachineByName(d.NomeMacchina) is IMachine) ? _machines.GetMachineByName(d.NomeMacchina).Id : 0;
                    var newl = new Layout
                    {
                        Color = d.Colore,
                        ItemCode = d.CodiceArticolo,
                        ItemDescription = d.DescrizioneArticolo,
                        LayoutNumber = d.NumDisposizione,
                        LayoutPhase = d.FaseDisposizione,
                        LayoutType = d.TipoDisposizione,
                        MachineNumber = d.NumeroMacchina,
                        MachineName = d.NomeMacchina,
                        Machine = machine,
                        Meters = d.MetriDisposti,
                        Quantity = d.PezzeDisposte,
                        StartTimestamp = d.DataDisposizione ?? DateTime.Now,
                        Status = Status.Waiting
                    };
                    _dB.Layouts.Add(newl);
                }
            }
            await _dB.SaveChangesAsync();

            //reload layouts
            layouts = await _dB.Layouts.Include(l => l.LayoutActRecords).ThenInclude(lr => lr.Fields).OrderByDescending(l => l.StartTimestamp).ToListAsync();
            var machines = _machines.GetMachines().ToList();
            var m = new IndexLayoutModel
            {
                Layouts = layouts,
                Machines = machines
            };
            return View(m);
        }

        [HttpGet]
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

    }
}
