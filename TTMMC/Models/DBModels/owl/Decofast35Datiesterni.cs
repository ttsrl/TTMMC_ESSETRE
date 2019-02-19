using System;
using System.Collections.Generic;

namespace TTMMC_ESSETRE.Models
{
    public partial class Decofast35Datiesterni
    {
        public Decofast35Datiesterni()
        {
            Decofast35Getvalue = new HashSet<Decofast35Getvalue>();
        }

        public int Id { get; set; }
        public string NomeMacchina { get; set; }
        public int? NumeroMacchina { get; set; }
        public string TipoDisposizione { get; set; }
        public long NumDisposizione { get; set; }
        public DateTime? DataDisposizione { get; set; }
        public int? CodiceArticolo { get; set; }
        public string DescrizioneArticolo { get; set; }
        public string Colore { get; set; }
        public int? MetriDisposti { get; set; }
        public int? PezzeDisposte { get; set; }
        public int? FaseDisposizione { get; set; }

        public virtual ICollection<Decofast35Getvalue> Decofast35Getvalue { get; set; }
    }
}
