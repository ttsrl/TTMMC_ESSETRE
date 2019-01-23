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
    public class MasterController : Controller
    {
        private readonly DBContext _dB;
        private readonly Utilities _utils;

        public MasterController(DBContext dB, Utilities utilities)
        {
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
            _utils = utilities ?? throw new ArgumentNullException(nameof(utilities));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var masters = await _dB.Masters.ToListAsync();
            var m = new IndexMasterModel
            {
                Masters = masters
            };
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(string code, string name, string hexColor)
        {
            if(!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(hexColor))
            {
                var color = _utils.ColorFromHex(hexColor);
                var existCode = await _dB.Masters.Where(m => m.Code == code.ToTrim()).CountAsync();
                if (existCode == 0)
                {
                    var master = new Master
                    {
                        Code = code,
                        Name = name,
                        Color = color
                    };
                    _dB.Masters.Add(master);
                    await _dB.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Error", new { id = 5 });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string code, string name, string hexColor)
        {
            if (id != 0 && !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(hexColor))
            {
                var master = await _dB.Masters.FindAsync(id);
                if (master is Master)
                {
                    var existCode = await _dB.Masters.Where(m => m.Code == code.ToTrim() && m.Id != id).CountAsync();
                    if (existCode == 0)
                    {
                        var color = _utils.ColorFromHex(hexColor);
                        master.Code = code;
                        master.Name = name;
                        master.Color = color;

                        await _dB.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index", "Error", new { id = 6 });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                var master = await _dB.Masters.FindAsync(id);
                if (master is Master)
                {
                    _dB.Masters.Remove(master);
                    await _dB.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }

    }
}