using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTMMC.Models;
using TTMMC.Services;

namespace TTMMC.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/GetLayoutLogsCount")]
    public class GetLayoutLogsCountController : ControllerBase
    {
        private readonly DBContext _dB;

        public GetLayoutLogsCountController(DBContext dB)
        {
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ls = await _dB.Layouts.Include(l => l.LayoutRecords).Where(l => l.Status == Models.DBModels.Status.Recording).ToListAsync();
            if (ls != null && ls.Count > 0)
            {
                var out_ = new Dictionary<string, int>();
                foreach (var l in ls)
                {
                    out_.Add(l.Barcode, l.LayoutRecords.Count());
                }
                return Ok(out_);
            }
            return NotFound(new { });
        }
    }
}
