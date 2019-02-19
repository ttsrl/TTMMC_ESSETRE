using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TTMMC_ESSETRE.Models;
using TTMMC_ESSETRE.Services;

namespace TTMMC_ESSETRE.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/PingMachine")]
    public class PingMachineController : ControllerBase
    {
        private readonly MachinesService _machinesService;
        public PingMachineController(MachinesService machinesService)
        {
            _machinesService = machinesService ?? throw new ArgumentNullException(nameof(machinesService));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            if (id != 0)
            {
                var machine = _machinesService.GetMachineById(id);
                if (machine is IMachine)
                {
                    long time = 0;
                    bool result = false;
                    try
                    {
                        var ping = new Ping();
                        var pr = ping.Send(machine.Address);
                        result = (pr.Status == IPStatus.Success);
                        time = pr.RoundtripTime;
                    }
                    catch { }
                    return Ok(new { result, time });
                }
            }
            return NotFound(new { });
        }
    }
}
