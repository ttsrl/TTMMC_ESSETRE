using TTMMC_ESSETRE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMMC_ESSETRE.ConfigurationModels
{
    public class Machine
    {
        public string Description { get; set; }
        public int Id { get; set; }
        private MachineType type;
        public MachineType Type { get => type; set => type = ((Enum.IsDefined(typeof(MachineType), value) ? value : MachineType.Null)); }
        public string ReferenceName { get; set; }
        private ConnectionProtocol protocol;
        public ConnectionProtocol Protocol { get => protocol; set => protocol = ((Enum.IsDefined(typeof(ConnectionProtocol), value) ? value : ConnectionProtocol.Null)); }
        public string Address { get; set; }
        public string Port { get; set; }
        public string Image { get; set; }
        public Dictionary<string, List<DataItem>> DatasAddressToRead { get; set; }
        public Dictionary<string, List<DataItem>> DatasAddressToWrite { get; set; }
    }

    public class DataItem
    {
        private string _dataType = "";
        public string Description { get; set; }
        public string Address { get; set; }
        public string DataType { get => _dataType.ToLower(); set => _dataType = value?.ToLower(); }
    }
}
