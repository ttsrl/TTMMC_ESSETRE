using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using TTMMC.Models.ViewModels;
using TTMMC.Services;

namespace TTMMC.Controllers
{
    public class PdfController : Controller
    {
        private readonly DBContext _dB;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PdfController(DBContext dB, IHostingEnvironment hostingEnvironment)
        {
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        [HttpGet]
        public async Task<IActionResult> MouldModule(int id)
        {
            var mould = await _dB.Moulds
                .Include(m => m.DefaultClient)
                .Include(m => m.DefaultMixture)
                    .ThenInclude(mm => mm.Items)
                .FirstOrDefaultAsync(m => m.Id == id);
            var pdf = new ViewAsPdf
            {
                PageOrientation = Orientation.Portrait,
                PageSize = Size.A4,
                PageWidth = 210,
                PageHeight = 297,
                PageMargins = new Margins(0, 0, 0, 0),
                Model = new PDFMouldModuleModel { Mould = mould },
                IsLowQuality = false,
                CustomSwitches = "--disable-smart-shrinking"
            };
            //ControllerContext.HttpContext.Response.Headers.Add("Content-Disposition", "inline");
            return pdf;
        }
    }
}