using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TTMMC_ESSETRE.ConfigurationModels;
using TTMMC_ESSETRE.Models;
using TTMMC_ESSETRE.Models.DBModels;
using TTMMC_ESSETRE.Models.ViewModels;
using TTMMC_ESSETRE.Services;

namespace TTMMC_ESSETRE.Controllers
{
    public class RecipeController : Controller
    {
        private readonly MachinesService _machines;
        private readonly TTMMCContext _dB;

        public RecipeController([FromServices] MachinesService machines, TTMMCContext dB)
        {
            _machines = machines;
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await _dB.Recipes.Include(r => r.RepiceSettings).ThenInclude(rr => rr.Fields).ToListAsync();
            var m = new IndexRecipeModel
            {
                Recipes = recipes
            };
            return View(m);
        }

        [HttpGet]
        public IActionResult New()
        {
            var machine = _machines.GetMachineById(1);
            var settings = machine.GetParametersWrite();
            var m = new NewRecipeModel
            {
                Fields = settings
            };
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(string name, Dictionary<string, string> fields, string notes)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var setts = new List<LayoutRecordField>();
                foreach (var f in fields)
                {
                    var it = new Dictionary<string, string>();
                    it.Add("0", f.Value);
                    var rf = new LayoutRecordField
                    {
                        Key = f.Key,
                        Value = JsonConvert.SerializeObject(it)
                    };
                    setts.Add(rf);
                }
                if (setts.Count > 0)
                {
                    var recipe = new Recipe
                    {
                        Name = name,
                        RepiceSettings = new LayoutRecord { Fields = setts },
                        Notes = notes
                    };
                    _dB.Recipes.Add(recipe);
                    await _dB.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Error", new { id = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                var recipe = await _dB.Recipes.Include(r => r.RepiceSettings).ThenInclude(rs => rs.Fields).FirstOrDefaultAsync(r => r.Id == id);
                var machine = _machines.GetMachineById(1);
                if (recipe is Recipe && machine is IMachine)
                {
                    var model = new EditRecipeViewModel { Recipe = recipe, Machine = machine };
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, Dictionary<string, string> fields, string notes)
        {
            if (id != 0)
            {
                var recipe = await _dB.Recipes.Include(r => r.RepiceSettings).ThenInclude(rs => rs.Fields).FirstOrDefaultAsync(r => r.Id == id);
                if (recipe is Recipe)
                {
                    recipe.Name = name;
                    foreach (var f in recipe.RepiceSettings.Fields)
                    {
                        if (fields.ContainsKey(f.Key))
                        {
                            f.Value = fields[f.Key];
                        }
                    }
                    recipe.Notes = notes;
                    await _dB.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Error", new { id = 2 });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                var recipe = await _dB.Recipes.Include(r => r.RepiceSettings).ThenInclude(rs => rs.Fields).FirstOrDefaultAsync(r => r.Id == id);
                if (recipe is Recipe)
                {
                    _dB.LayoutsRecordsFields.RemoveRange(recipe.RepiceSettings.Fields);
                    _dB.LayoutsRecords.Remove(recipe.RepiceSettings);
                    _dB.Recipes.Remove(recipe);
                    await _dB.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Error", new { id = 3 });
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            if (id != 0)
            {
                var recipe = await _dB.Recipes.Include(r => r.RepiceSettings).ThenInclude(rs => rs.Fields).FirstOrDefaultAsync(r => r.Id == id);
                if (recipe is Recipe)
                {
                    return RedirectToAction("ViewRecipe", "PDF", new { id });
                }
            }
            return RedirectToAction("Index");
        }
    }
}