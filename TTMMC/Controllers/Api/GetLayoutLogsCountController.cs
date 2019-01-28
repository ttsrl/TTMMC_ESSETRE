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
        private readonly LayoutListener _lListener;
        private readonly DBContext _dB;

        public GetLayoutLogsCountController(DBContext dB, LayoutListener lListener)
        {
            _dB = dB ?? throw new ArgumentNullException(nameof(dB));
            _lListener = lListener ?? throw new ArgumentNullException(nameof(lListener));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ls = await _dB.Layouts.Where(l => l.Status == Models.DBModels.Status.Recording).Select(l => new { l.Id, l.Barcode }).ToListAsync();
            if (ls != null && ls.Count > 0)
            {
                var out_ = new Dictionary<string, long>();
                foreach (var l in ls)
                {
                    var ll = _lListener.GetLayoutListenItemById(l.Id);
                    if(ll is LayoutListenItem)
                    {
                        out_.Add(l.Barcode, ll.WorkCount);
                    }
                }
                return Ok( out_ );
            }
            return NotFound(new { });
        }
    }
}
