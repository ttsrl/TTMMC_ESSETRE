using System;
using System.Collections.Generic;
using TTMMC.Services;
using TTMMC.Utils;

namespace TTMMC.Models.DBModels
{
    public enum Package
    {
        Nessuna,
        GabbiaRossa,
        GabbiaVerde,
        Sacco,
        ScatolaGrande,
        ScatolaMedia,
        ScatolaPiccola,
        Cesta
    }

    public enum Status
    {
        Waiting,
        Recording,
        Stopped,
        Finished
    }

    public class Layout
    {
        private string _minced = "";
        private DateTime? _startTimestamp;

        public int Id { get; set; }
        public Status Status { get; set; }
        public string Barcode { get; set; }
        public Client Client { get; set; }
        public Mould Mould { get; set; }
        public int Machine { get; set; }
        public Mixture Mixture { get; set; }
        public Master Master { get; set; }
        public int Quantity { get; set; }
        public string Minced { get => _minced; set => _minced = value?.ToTrim().ToTitleCase(); }
        public TimeSpan Humidification { get; set; }
        public Package Packaging { get; set; }
        public int PackagingQuantity { get; set; }
        public DateTime Start { get; set; }
        public List<LayoutRecord> LayoutActRecords { get; set; }
        public LayoutRecord LayoutSetRecord { get; set; }
        public string Notes { get; set; }
        public DateTime StartTimestamp
        {
            get => _startTimestamp ?? DateTime.Now;
            set => _startTimestamp = value;
        }
    }
}
