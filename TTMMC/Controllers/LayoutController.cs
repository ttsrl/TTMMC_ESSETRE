using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMMC.Models;
using TTMMC.Models.DBModels;
using TTMMC.Models.ViewModels;
using TTMMC.Services;

namespace TTMMC.Controllers
{
    public class LayoutController : Controller
    {
        private readonly LayoutListener _lListener;
        private readonly DBContext _dB;
        private readonly MachinesService _machines;

        public LayoutController(MachinesService machines, DBContext dB, LayoutListener layoutListener)
        {
            _machines = machines ?? throw new ArgumentNullException(nameof(machines));
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
            _lListener = layoutListener ?? throw new ArgumentNullException(nameof(layoutListener));
        }

        public async Task<IActionResult> Index()
        {
            var l = await _dB.Layouts
                .Include(ll => ll.Client)
                .Include(ll => ll.Master)
                .Include(ll => ll.Mixture)
                    .ThenInclude(m => m.Items)
                .Include(ll => ll.Mould)
                .Include(ll => ll.LayoutRecords)
                .ToListAsync();
            var c = await _dB.Clients.ToListAsync();
            var model = new IndexLayoutModel
            {
                Layouts = l,
                Machines = _machines.GetMachines().ToList()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> New(int mould)
        {
            var clients = await _dB.Clients.ToListAsync();
            var masters = await _dB.Masters.ToListAsync();
            var moulds = await _dB.Moulds
                .Include(m => m.DefaultClient)
                .Include(m => m.DefaultMixture)
                .ToListAsync();
            var mixtures = await _dB.Mixtures
                .Include(m => m.Items)
                .ThenInclude(m => m.Material)
                .ToListAsync();
            var packaging = Enum.GetValues(typeof(Package)).Cast<Package>().ToDictionary(t => (int)t, t => t.ToString());
            var defM = moulds.FirstOrDefault(m => m.Id == mould);
            var model = new NewLayoutViewModel
            {
                Machines = _machines.GetMachines().ToList(),
                Clients = clients,
                Moulds = moulds,
                Masters = masters,
                Mixtures = mixtures,
                Packaging = packaging,
                DefaultClient = (defM is Mould) ? defM.DefaultClient : null,
                DefaultMixture = (defM is Mould) ? defM.DefaultMixture : null,
                DefaultMould = (defM is Mould) ? defM : null
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([FromServices] Barcode barcode, [FromServices] Utilities _utils, NewLayoutModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Client > 0 && model.Mould > 0 && model.Master > 0 && model.Mixture > 0 && model.Quantity > 0 && model.Machine > 0)
                {
                    var client = await _dB.Clients.FirstOrDefaultAsync(c => c.Id == model.Client);
                    var mould = await _dB.Moulds.FirstOrDefaultAsync(c => c.Id == model.Mould);
                    var master = await _dB.Masters.FirstOrDefaultAsync(c => c.Id == model.Master);
                    var mixture = await _dB.Mixtures.FirstOrDefaultAsync(c => c.Id == model.Mixture);
                    var machine = _machines.GetMachineById(model.Machine);
                    if (client is Client && mould is Mould && master is Master && mixture is Mixture && machine is IMachine)
                    {
                        var codes = await _dB.Layouts.Select(c => c.Barcode).ToListAsync();
                        var layout = new Layout
                        {
                            Barcode = barcode.CreateNewEan13(codes),
                            Client = client,
                            Mould = mould,
                            Master = master,
                            Mixture = mixture,
                            Quantity = model.Quantity,
                            Machine = model.Machine,
                            Notes = model.Notes,
                            Packaging = model.Packaging,
                            PackagingQuantity = model.PackagingCount,
                            Status = Status.Waiting,
                            Minced = (model.MincedCheck == 1) ? model.Minced : null,
                            Humidification = (model.HumidifiedCheck == 1) ? TimeSpan.FromMinutes(model.Humidified) : TimeSpan.Zero,
                            Start = model.Start
                        };
                        _dB.Layouts.Add(layout);
                        await _dB.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index", "Error", new { id = 11 });
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
                var layout = await _dB.Layouts.Where(l => l.Status == Status.Finished).FirstOrDefaultAsync(l => l.Id == id);
                if(layout is Layout)
                {

                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if(id != 0)
            {
                var layout = await _dB.Layouts
                    .Include(l => l.LayoutRecords)
                    .FirstOrDefaultAsync(l => l.Id == id);
                if (layout is Layout)
                {
                    _dB.LayoutsRecords.RemoveRange(layout.LayoutRecords);
                    _dB.Layouts.Remove(layout);
                    await _dB.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}