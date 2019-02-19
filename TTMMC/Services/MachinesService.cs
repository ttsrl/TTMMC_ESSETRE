using Hylasoft.Opc.Ua;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TTMMC_ESSETRE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTMMC_ESSETRE.ConfigurationModels;

namespace TTMMC_ESSETRE.Services
{
    public enum ConnectionProtocol
    {
        Null,
        OPCUA
    }

    public enum MachineType
    {
        Null,
        Decofast,
    }

    public enum MachineStatus
    {
        Offline,
        Online
    }

    public class MachinesService
    {
        private List<IMachine> machines = new List<IMachine>();
        public int Count { get => machines.Count(); }
        private readonly Utilities _utils;
        
        public MachinesService([FromServices] Utilities utils)
        {
            _utils = utils;

            var configMachines = _utils.GetConfigurationElementsList<Machine>("Machines");
            if(configMachines.Count > 0)
            {
                foreach(var m in configMachines)
                {
                    if (m.Protocol == ConnectionProtocol.Null || m.Type == MachineType.Null)
                        continue;
                    if(m.Protocol == ConnectionProtocol.OPCUA)
                    {
                        var client = new OPCMachine(m);
                        client.Connect();
                        machines.Add(client);
                    }
                }
            }
        }

        public IEnumerable<IMachine> GetMachines()
        {
            return machines;
        }

        public IMachine GetMachineById(int id)
        {
            foreach (var m in machines)
            {
                if (m.Id == id)
                {
                    return m;
                }
            }
            return null;
        }

        //public OPCMachine GetMachineByName(string Name)
        //{
        //    if (!string.IsNullOrEmpty(Name))
        //    {
        //        var machine = Machines[Name];
        //        return machine;
        //    }
        //    return null;
        //}

        //public string MachineStatusJson
        //{
        //    get
        //    {
        //        return JsonConvert.SerializeObject(Enum.GetValues(typeof(OPCMachine.MachineStatus)), new Newtonsoft.Json.Converters.StringEnumConverter());
        //    }
        //}

    }
}
