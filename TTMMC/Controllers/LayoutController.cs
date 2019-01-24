using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTMMC.Models.DBModels;
using TTMMC.Models.ViewModels;
using TTMMC.Services;

namespace TTMMC.Controllers
{
    public class LayoutController : Controller
    {
        private readonly LayoutListener _layoutListener;

        public LayoutController([FromServices] LayoutListener layoutListener)
        {
            _layoutListener = layoutListener ?? throw new ArgumentNullException(nameof(layoutListener));
        }

        public IActionResult Index()
        {
            var layout = new Layout
            {
                Id = 1,
                Machine = 1
            };
            var m = new IndexLayoutModel { Value = _layoutListener.GetWorkCount(layout) };
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewLayoutModel model)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StartListen()
        {
            var layout = new Layout
            {
                Id = 1,
                Machine = 1
            };
            _layoutListener.Add(layout);
            _layoutListener.Start(layout);
            return  RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> StopListen()
        {
            var layout = new Layout
            {
                Id = 1,
                Machine = 1
            };
            _layoutListener.Stop(layout);
            return RedirectToAction("Index");
        }
    }
}