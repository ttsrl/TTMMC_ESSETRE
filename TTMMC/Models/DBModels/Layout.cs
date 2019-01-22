using System;
using System.Collections.Generic;

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

    public class Layout
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public Client Client { get; set; }
        public Mould Mould { get; set; }
        public int Machine { get; set; }
        public Mixture Mixture { get; set; }
        public Master Master { get; set; }
        public int Quantity { get; set; }
        public string Minced { get; set; }
        public TimeSpan Umidification { get; set; }
        public Package Packaging { get; set; }
        public int PackagingQuantity { get; set; }
        public DateTime Start { get; set; }
    }
}
