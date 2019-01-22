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
            //var mix = await _dB.Mixtures
            //    .Include(mx => mx.Items)
            //        .ThenInclude(i => i.Material)
            //    .ToListAsync();
            var ms = await _dB.Materials.ToListAsync();
            var mix = new List<Mixture>();
            mix.Add(new Mixture { Name = "prova", Notes = "prova prova prova prova prova prova prova", Items = new List<MixtureItem> { new MixtureItem { Material = ms.Where(m => m.Id == 1).FirstOrDefault(), Quantity = 1 }, new MixtureItem { Material = ms.Where(m => m.Id == 2).FirstOrDefault(), Quantity = 2 } } });
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
            return RedirectToAction("Index");
        }
    }
}