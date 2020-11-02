using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTMMC_ESSETRE.Services;
using TTMMC_ESSETRE.Models;
using TTMMC_ESSETRE.Utils;
using System.Diagnostics;

namespace TTMMC_ESSETRE.Controllers.Api
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
            if (machine is IMachine)
            {
                var out_ = new Dictionary<string, Dictionary<string, string>>();
                if (machine.Status == MachineStatus.Online)
                {
                    foreach (var elm in machine.GetParametersRead())
                    {
                        var elmL = new Dictionary<string, string>();
                        var c = 0;
                        foreach (var k in elm.Value)
                        {
                            var val = "";
                            try
                            {
                                var type = machine.GetDataItemType(k);
                                val = machine.Read(k.Address, type);
                                if (type != typeof(string))
                                {
                                    if (type == typeof(short) || type == typeof(ushort) || type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong))
                                    {
                                        if (k.Scaling > 0)
                                        {
                                            double floatVal = double.Parse(val);
                                            for (var i = 0; i < k.Scaling; i++)
                                            {
                                                floatVal = floatVal / 10.0;
                                            }
                                            val = floatVal.ToString();
                                        }
                                    }
                                    if ((val.Contains(",") || val.Contains(".")))
                                    {
                                        var decimalVal = double.Parse(val);
                                        val = Math.Round(decimalVal, 2).ToString();
                                    }
                                }
                            }
                            catch { }
                            elmL.Add(c.ToString(), val);
                            c++;
                        }
                        out_.Add(elm.Key.ToNotMappedAttribute(), elmL);
                    }
                    return Ok(new { parameters = out_ });
                }
                return NotFound(new { });
            }
            return NotFound(new { });
        }
    }
}