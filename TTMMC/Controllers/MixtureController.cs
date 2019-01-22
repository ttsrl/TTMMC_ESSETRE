using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMMC.Models.DBModels;
using TTMMC.Models.ViewModels;
using TTMMC.Services;

namespace TTMMC.Controllers
{
    public class MixtureController : Controller
    {
        private readonly DBContext _dB;

        public MixtureController(DBContext dB)
        {
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var mix = await _dB.Mixtures
                .Include(mx => mx.Items)
                    .ThenInclude(i => i.Material)
                .ToListAsync();
            var ms = await _dB.Materials.ToListAsync();
            var model = new IndexMixtureModel
            {
                Materials = ms,
                Mixtures = mix
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(string name, Dictionary<string, int> quantitys, Dictionary<string, int> materials, string notes)
        {
            if (!string.IsNullOrEmpty(name) && quantitys.Count > 0 && materials.Count > 0 && quantitys.Count == materials.Count)
            {
                var mats = await _dB.Materials.ToListAsync();
                if (mats != null && mats.Count > 0)
                {
                    var mixItems = new List<MixtureItem>();
                    foreach (var q in quantitys)
                    {
                        var mat = mats.Where(m => m.Id == materials[q.Key]).FirstOrDefault();
                        var it = new MixtureItem
                        {
                            Quantity = q.Value,
                            Material = mat
                        };
                        mixItems.Add(it);
                    }
                    var mix = new Mixture
                    {
                        Name = name,
                        Items = mixItems,
                        Notes = notes
                    };

                    _dB.Mixtures.Add(mix);
                    await _dB.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if(id != 0)
            {
                var mixt = await _dB.Mixtures
                           .Include(m => m.Items)
                               .ThenInclude(it => it.Material)
                           .Where(m => m.Id == id)
                           .FirstOrDefaultAsync();
                if(mixt is Mixture)
                {
                    _dB.MixtureItems.RemoveRange(mixt.Items);
                    _dB.Mixtures.Remove(mixt);
                    await _dB.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, Dictionary<string, int> quantitys, Dictionary<string, int> materials, string notes)
        {
            if (id != 0 && !string.IsNullOrEmpty(name) && quantitys.Count > 0 && materials.Count > 0 && quantitys.Count == materials.Count)
            {
                var mixt = await _dB.Mixtures
                            .Include(m => m.Items)
                                .ThenInclude(it => it.Material)
                            .Where(m => m.Id == id)
                            .FirstOrDefaultAsync();
                var mats = await _dB.Materials.ToListAsync();
                if (mixt is Mixture && mats != null && mats.Count > 0)
                {
                    var items = new List<MixtureItem>();
                    foreach (var q in quantitys)
                    {
                        var mat = mats.Where(m => m.Id == materials[q.Key]).FirstOrDefault();
                        var contains = mixt.Items.Select(i => new { i.Material, i.Quantity }).Where(i => i.Material == mat && i.Quantity == q.Value).Count() > 0 ? true : false;
                        if (contains)
                        {
                            items.Add(mixt.Items.Find(i => i.Quantity == q.Value && i.Material == mat));
                        }
                        else
                        {
                            var it = new MixtureItem
                            {
                                Quantity = q.Value,
                                Material = mat
                            };
                            items.Add(it);
                        }
                    }
                    foreach (var it in mixt.Items)
                    {
                        if (!items.Contains(it))
                        {
                            _dB.MixtureItems.Remove(it);
                        }
                    }
                    mixt.Name = name;
                    mixt.Items = items;
                    mixt.Notes = notes;
                    await _dB.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}