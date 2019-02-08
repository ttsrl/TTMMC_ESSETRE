using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMMC.Models.DBModels;
using TTMMC.Models.ViewModels;
using TTMMC.Services;
using TTMMC.Utils;

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
                .OrderByDescending(mx => mx.Id)
                .ToListAsync();
            var ms = await _dB.Materials.OrderBy(c => c.Name).ToListAsync();
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
                var exist = await _dB.Mixtures.Where(m => m.Name == name.ToTrim().ToFirstCharUpper()).CountAsync();
                if (exist == 0)
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
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index", "Error", new { id = 9 });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, Dictionary<string, int> quantitys, Dictionary<string, int> materials, string notes)
        {
            if (id != 0 && !string.IsNullOrEmpty(name) && quantitys.Count > 0 && materials.Count > 0 && quantitys.Count == materials.Count)
            {
                var exist = await _dB.Mixtures.Where(m => m.Name == name.ToTrim().ToFirstCharUpper() && m.Id != id).CountAsync();
                if (exist == 0)
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
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index", "Error", new { id = 10 });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                var mixt = await _dB.Mixtures
                           .Include(m => m.Items)
                               .ThenInclude(it => it.Material)
                           .Where(m => m.Id == id)
                           .FirstOrDefaultAsync();
                if (mixt is Mixture)
                {
                    var lavok = await _dB.Layouts.Include(l => l.Mixture).Where(l => l.Mixture.Id == id).CountAsync();
                    if (lavok == 0)
                    {
                        var moulds = await _dB.Moulds.ToListAsync();
                        foreach (var m in moulds)
                        {
                            if (m.DefaultMixture == mixt)
                            {
                                m.DefaultMixture = null;
                            }
                        }
                        _dB.MixtureItems.RemoveRange(mixt.Items);
                        _dB.Mixtures.Remove(mixt);
                        await _dB.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index", "Error", new { id = 18 });
                }
            }
            return RedirectToAction("Index");
        }
    }
}