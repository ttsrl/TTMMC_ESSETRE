using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using TTMMC_ESSETRE.Models.DBModels;
using TTMMC_ESSETRE.Models.ViewModels;
using TTMMC_ESSETRE.Services;

namespace TTMMC_ESSETRE.Controllers
{
    public class PdfController : Controller
    {
        private readonly DBContext _dB;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly MachinesService _machines;

        public PdfController(DBContext dB, MachinesService machines, IHostingEnvironment hostingEnvironment)
        {
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _machines = machines ?? throw new ArgumentNullException(nameof(machines));
        }

       /* [HttpGet]
        public async Task<IActionResult> Report(int id)
        {
            var layout = await _dB.Layouts
                .Include(l => l.Client)
                .Include(l => l.LayoutActRecords)
                    .ThenInclude(lr => lr.Fields)
                .Include(l => l.LayoutSetRecord)
                    .ThenInclude(lr => lr.Fields)
                .Include(l => l.Master)
                .Include(l => l.Mixture)
                    .ThenInclude(m => m.Items)
                        .ThenInclude(m => m.Material)
                .Include(l => l.Mould)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (layout is Layout)
            {
                var machine = _machines.GetMachineById(layout.Machine);
                var pdf = new ViewAsPdf
                {
                    PageOrientation = Orientation.Portrait,
                    PageSize = Size.A4,
                    PageWidth = 210,
                    PageHeight = 297,
                    PageMargins = new Margins(1, 1, 1, 1),
                    Model = new PDFReportModel { Layout = layout, Machine = machine },
                    IsLowQuality = false,
                    CustomSwitches = "--disable-smart-shrinking",
                    FileName = "report_" + layout.Barcode + ".pdf",
                    ContentDisposition = ContentDisposition.Inline,
                    ContentType = "application/pdf"
                };
                return pdf;
            }
            return RedirectToAction("Index", "Error", new { id = 16 });
        }*/

    }
}