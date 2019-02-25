using Hylasoft.Opc.Ua;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTMMC_ESSETRE.Services;
using TTMMC_ESSETRE.ConfigurationModels;
using Hylasoft.Opc.Common;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TTMMC_ESSETRE.Models
{
    partial class OPCMachine : IMachine
    {
        private bool recording = false;
        private UaClient uaClient;
        private bool firstConnection = false;
        private Dictionary<string, List<DataItem>> datasAddressToRead = new Dictionary<string, List<DataItem>>();
        private Dictionary<string, List<DataItem>> datasAddressToWrite = new Dictionary<string, List<DataItem>>();
        private string imgLink;
        
        public string Description { get; }
        public int Id { get; }
        public string Address { get; }
        public string Port { get; }
        public string ReferenceName { get; }
        public MachineType Type { get; }
        public ConnectionProtocol ConnectionProtocol { get; }
        public bool HaveImage { get; }
        public bool Recording { get => recording; set => recording = value; }

        public OPCMachine(Machine machine)
        {
            Type = machine.Type;
            ReferenceName = machine.ReferenceName;
            Description = machine.Description;
            Address = machine.Address;
            Id = machine.Id;
            Port = machine.Port;
            ConnectionProtocol = machine.Protocol;
            HaveImage = (!string.IsNullOrEmpty(machine.Image)) ? true : false;
            imgLink = (!string.IsNullOrEmpty(machine.Image)) ? machine.Image : null;
            datasAddressToRead = machine.DatasAddressToRead ?? new Dictionary<string, List<DataItem>>();
            datasAddressToWrite = machine.DatasAddressToWrite ?? new Dictionary<string, List<DataItem>>();
            uaClient = new UaClient(new Uri("opc.tcp://" + Address + ":" + Port));
            uaClient.ServerConnectionLost += _uaClient_ServerConnectionLost;
        }

        public KeyValuePair<string, List<DataItem>>? GetReferenceKeyRead()
        {
            foreach (var d in datasAddressToRead)
            {
                var isRef = (d.Key.Substring(0, 1) == "{" && d.Key.Substring(d.Key.Length - 1, 1) == "}");
                if (isRef)
                    return d;
            }
            return null;
        }

        public KeyValuePair<string, List<DataItem>>? GetReferenceKeyWrite()
        {
            foreach (var d in datasAddressToWrite)
            {
                var isRef = (d.Key.Substring(0, 1) == "{" && d.Key.Substring(d.Key.Length - 1, 1) == "}");
                if (isRef)
                    return d;
            }
            return null;
        }

        private void _uaClient_ServerConnectionLost(object sender, EventArgs e)
        {
            try
            {
                if (uaClient.Status == Hylasoft.Opc.Common.OpcStatus.Connected)
                    return;
                uaClient.ReConnect();
            }catch{ }
        }

        public void Connect()
        {
            try
            {
                if (uaClient.Status == Hylasoft.Opc.Common.OpcStatus.Connected)
                    return;
                if (!firstConnection)
                {
                    uaClient.Connect();
                    firstConnection = true;
                }
                else
                {
                    uaClient.RecreateSession();
                }
            }
            catch {
                
            }
            
        }

        public MachineStatus GetStatus()
        {
            if(uaClient.Status == Hylasoft.Opc.Common.OpcStatus.Connected)
            {
                return MachineStatus.Online;
            }
            else
            {
                return MachineStatus.Offline;
            }
        }

        public string GetImageUrl()
        {
            return imgLink;
        }

        public async Task<T> ReadAsync<T>(string key)
        {
            object value = await uaClient.ReadAsync<T>(key);
            var vl = (ReadEvent<T>)value;
            return (T)Convert.ChangeType(vl.Value, typeof(T));
        }

        public async Task<string> ReadAsync(string key, Type type)
        {
            if (type == typeof(int))
            {
                var val = await uaClient.ReadAsync<int>(key);
                return val.Value.ToString();
            }
            else if (type == typeof(uint))
            {
                var val = await uaClient.ReadAsync<uint>(key);
                return val.Value.ToString();
            }
            else if (type == typeof(long))
            {
                var val = await uaClient.ReadAsync<long>(key);
                return val.Value.ToString();
            }
            else if (type == typeof(ulong))
            {
                var val = await uaClient.ReadAsync<ulong>(key);
                return val.Value.ToString();
            }
            else if (type == typeof(float))
            {
                var val = await uaClient.ReadAsync<float>(key);
                return val.Value.ToString();
            }
            else if (type == typeof(double))
            {
                var val = await uaClient.ReadAsync<double>(key);
                return val.Value.ToString();
            }
            else if (type == typeof(string))
            {
                var val = await uaClient.ReadAsync<string>(key);
                return val.Value.ToString();
            }
            return null;
        }

        public T Read<T>(string key)
        {
            object value = uaClient.Read<T>(key).Value;
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public string Read(string key, Type type)
        {
            if(type == typeof(int))
            {
                var val = uaClient.Read<int>(key).Value;
                return val.ToString();
            }
            else if(type == typeof(uint))
            {
                var val = uaClient.Read<uint>(key).Value;
                return val.ToString();
            }
            else if (type == typeof(long))
            {
                var val = uaClient.Read<long>(key).Value;
                return val.ToString();
            }
            else if (type == typeof(ulong))
            {
                var val = uaClient.Read<ulong>(key).Value;
                return val.ToString();
            }
            else if (type == typeof(float))
            {
                var val = uaClient.Read<float>(key).Value;
                return val.ToString();
            }
            else if (type == typeof(double))
            {
                var val = uaClient.Read<double>(key).Value;
                return val.ToString();
            }
            else if (type == typeof(string))
            {
                var val = uaClient.Read<string>(key).Value;
                return val.ToString();
            }
            return null;
        }

        public void Write<T>(string key, T item)
        {
            uaClient.Write<T>(key, item);
        }

        public async void WriteAsync<T>(string key, T item)
        {
            await uaClient.WriteAsync<T>(key, item);
        }

        public Type GetDataItemType(DataItem data)
        {
            if(data.DataType == "int")
            {
                return typeof(int);
            }
            else if (data.DataType == "uint")
            {
                return typeof(uint);
            }
            else if (data.DataType == "dint")
            {
                return typeof(long);
            }
            else if (data.DataType == "udint")
            {
                return typeof(ulong);
            }
            else if (data.DataType == "real")
            {
                return typeof(float);
            }
            else if (data.DataType == "double")
            {
                return typeof(double);
            }
            else if (data.DataType == "string")
            {
                return typeof(string);
            }
            return null;
        }

        public Dictionary<string, List<DataItem>> GetParametersRead()
        {
            return datasAddressToRead;
        }

        public Dictionary<string, List<DataItem>> GetParametersWrite()
        {
            return datasAddressToWrite;
        }

        public List<DataItem> GetParameterRead(string name)
        {
            foreach(var p in datasAddressToRead)
            {
                if(p.Key == name)
                {
                    return p.Value;
                }
            }
            return null;
        }

        public List<DataItem> GetParameterWrite(string name)
        {
            foreach (var p in datasAddressToWrite)
            {
                if (p.Key == name)
                {
                    return p.Value;
                }
            }
            return null;
        }

    }
}
