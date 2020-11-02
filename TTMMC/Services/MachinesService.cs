using Microsoft.AspNetCore.Mvc;
using TTMMC_ESSETRE.Models;
using System.Collections.Generic;
using System.Linq;
using TTMMC_ESSETRE.ConfigurationModels;
using System.Threading;

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

        private static bool started = false;
        public static bool Started { get => started; }

        private System.Timers.Timer autoReconnectionTimer;

        public MachinesService([FromServices] Utilities utils)
        {
            _utils = utils;
            autoReconnectionTimer = new System.Timers.Timer(4000);
            autoReconnectionTimer.AutoReset = true;
            autoReconnectionTimer.Elapsed += AutoReconnectionTimer_Elapsed;
            autoReconnectionTimer.Start();

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
                        client.ConnectAsync();
                        machines.Add(client);
                    }
                }
            }
        }

        private void AutoReconnectionTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var thred = new Thread(autoReconnect);
            thred.Start();
        }

        private void autoReconnect()
        {
            foreach (var m in machines)
            {
                if (m.Status == MachineStatus.Offline)
                    m.ConnectAsync();
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

        public IMachine GetMachineByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                foreach (var m in machines)
                {
                    if (m.ReferenceName == Name)
                    {
                        return m;
                    }
                }
            }
            return null;
        }

    }
}
