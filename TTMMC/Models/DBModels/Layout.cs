using System;
using TTMMC.Services;

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
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Barcode { get; set; }
        public Client Client { get; set; }
        public Mould Mould { get; set; }
        public int Machine { get; set; }
        public Mixture Mixture { get; set; }
        public Master Master { get; set; }
        public int Quantity { get; set; }
        public string Minced { get; set; }
        public TimeSpan Humidification { get; set; }
        public Package Packaging { get; set; }
        public int PackagingQuantity { get; set; }
        public DateTime Start { get; set; }
        public string Notes { get; set; }
    }
}
