using System;
using System.Collections.Generic;

namespace TTMMC_ESSETRE.Models
{
    public partial class Decofast35Lavorazionemacchina
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int LavorazioneMacchinaId { get; set; }

        public virtual Decofast35Getvalue LavorazioneMacchina { get; set; }
    }
}
