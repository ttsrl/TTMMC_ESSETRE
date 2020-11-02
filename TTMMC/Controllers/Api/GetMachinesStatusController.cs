using Microsoft.AspNetCore.Mvc;
using TTMMC_ESSETRE.Services;
using System;
using System.Collections.Generic;
namespace TTMMC_ESSETRE.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/GetMachinesStatus")]
    public class GetMachinesStatusController : ControllerBase
    {
        private readonly MachinesService _machinesService;
        public GetMachinesStatusController(MachinesService machinesService)
        {
            _machinesService = machinesService ?? throw new ArgumentNullException(nameof(machinesService));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var r = new List<object>();
            foreach (var m in _machinesService.GetMachines())
            {
                r.Add(new { key = m.ReferenceName, value = m.Status });
            }
            return Ok(r);
        }
    }
}
