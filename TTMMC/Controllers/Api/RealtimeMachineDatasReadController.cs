using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTMMC.Services;
using Newtonsoft.Json;
using TTMMC.Models;
using TTMMC.Utils;

namespace TTMMC.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/RealtimeMachineDatasRead")]
    public class RealtimeMachineDatasReadController : ControllerBase
    {
        private readonly MachinesService _machineService;
        private readonly Utilities _utils;
        public RealtimeMachineDatasReadController(Utilities utils, MachinesService machineService)
        {
            _machineService = machineService ?? throw new ArgumentNullException(nameof(machineService));
            _utils = utils ?? throw new ArgumentNullException(nameof(utils));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var machine = _machineService.GetMachineById(id);
            if(machine is IMachine)
            {
                var out_ = new Dictionary<string, Dictionary<string, string>>();
                if (machine.GetStatus() == MachineStatus.Online)
                {
                    foreach (var elm in machine.GetParametersRead())
                    {
                        var elmL = new Dictionary<string, string>();
                        var c = 0;
                        foreach (var k in elm.Value)
                        {
                            var type = machine.GetDataItemType(k);
                            var val = "";
                            try
                            {
                                val = _utils.ValueToString(type, machine.Read(k.Address, type) ?? "");
                            }
                            catch { }
                            elmL.Add(c.ToString(), val);
                            c++;
                        }
                        out_.Add(elm.Key.ToNotMappedAttribute(), elmL);
                    }
                }
                else
                {
                    machine.Connect();
                }
                return Ok(new { status = (int)machine.GetStatus(), parameters = out_ });
            }
            return NotFound(new { });
        }
    }
}
