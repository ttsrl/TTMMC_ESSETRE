using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTMMC_ESSETRE.Models.DBModels;
using TTMMC_ESSETRE.Services;

namespace TTMMC_ESSETRE.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/RealtimeLayouts")]
    public class RealtimeLayoutsController : ControllerBase
    {
        private readonly TTMMCContext _dB;

        public RealtimeLayoutsController(TTMMCContext dB)
        {
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ls = await _dB.Layouts.Include(l => l.LayoutActRecords).Select(l => new {l.LayoutNumber, l.Status, l.LayoutActRecords.Count }).Where(l => l.Status == Models.DBModels.Status.Recording || l.Status == Models.DBModels.Status.Finished).ToListAsync();
            if (ls != null && ls.Count > 0)
            {
                var out_ = new Dictionary<string, object>();
                foreach (var l in ls)
                {
                    out_.Add(l.LayoutNumber.ToString(), new { status = Enum.GetName(typeof(Status),l.Status), logs = l.Count });
                }
                return Ok(out_);
            }
            return NotFound(new { });
        }
    }
}
