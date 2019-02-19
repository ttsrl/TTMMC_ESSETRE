using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMMC_ESSETRE.Models.DBModels
{
    public enum Status
    {
        Waiting,
        Recording,
        Stopped,
        Finished
    }

    public class Layout
    {
        private DateTime? _timestamp;
        public int Id { get; set; }
        public string MachineName { get; set; }
        public int? MachineNumber { get; set; }
        public int Machine { get; set; }
        public string LayoutType { get; set; }
        public long LayoutNumber { get; set; }
        public int? LayoutPhase { get; set; }
        public int? ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string Color { get; set; }
        public int? Meters { get; set; }
        public int? Quantity { get; set; }
        public Status Status { get; set; }
        public List<LayoutRecord> LayoutActRecords { get; set; }
        public LayoutRecord LayoutSetRecord { get; set; }
        public DateTime StartTimestamp
        {
            get => _timestamp ?? DateTime.Now;
            set => _timestamp = value;
        }
    }
}
