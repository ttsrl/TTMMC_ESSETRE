using Hylasoft.Opc.Ua;
using Microsoft.AspNetCore.Mvc;
using TTMMC.Models;
using TTMMC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMMC.Controllers.Api
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
                var status = m.GetStatus();
                r.Add(new { key = m.ReferenceName, value = status });
                if (status == MachineStatus.Offline)
                {
                    m.Connect();
                }
            }
            return Ok(r);
        }
    }
}
