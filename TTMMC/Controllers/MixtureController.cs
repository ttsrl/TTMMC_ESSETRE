using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var mix = await _dB.Mixtures
                .Include(mx => mx.Items)
                    .ThenInclude(i => i.Material)
                .ToListAsync();
            var ms = await _dB.Materials.ToListAsync();
            var m = new IndexMixtureModel
            {
                Materials = ms,
                Mixtures = mix
            };
            return View(m);
        }

        public async Task<IActionResult> New()
        {
            
            return RedirectToAction("Index");
        }
    }
}