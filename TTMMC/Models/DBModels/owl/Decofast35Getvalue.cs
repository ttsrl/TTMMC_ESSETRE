using System;
using System.Collections.Generic;

namespace TTMMC_ESSETRE.Models
{
    public partial class Decofast35Getvalue
    {
        public Decofast35Getvalue()
        {
            Decofast35Lavorazionemacchina = new HashSet<Decofast35Lavorazionemacchina>();
        }

        public int Id { get; set; }
        public double VelMacchina { get; set; }
        public double? NominaleTensioneElettrFeltro { get; set; }
        public int NumDisposizioneId { get; set; }
        public double? AlimentAriaPressione { get; set; }
        public double? AspirazioneBocchetteDepress { get; set; }
        public double? CicloRaffreddMin { get; set; }
        public double? CicloRaffreddSec { get; set; }
        public double? ContamentriLunghezzAttualePrecedente { get; set; }
        public double? ContametriLunghezAttuale { get; set; }
        public double? CoppiaCilindroDecatit { get; set; }
        public double? CoppiaCilindroGommato01 { get; set; }
        public double? CoppiaCilindroGommato02 { get; set; }
        public double? GuarnizVascaPressione { get; set; }
        public double? PressGuarnizioneVasca { get; set; }
        public double? RiscaldamentoCilindroDecatitoreBar { get; set; }
        public double? RiscaldamentoCilindroDecatitoreGradi { get; set; }
        public double? RiscaldamentoCilindroDecatitoreMin { get; set; }
        public double? RiscaldamentoCilindroDecatitorePercent { get; set; }
        public double? RiscaldamentoCilindroDecatitoreSec { get; set; }
        public double? RiscaldamentoVascaMin { get; set; }
        public double? RiscaldamentoVascaSec { get; set; }
        public double? TempoLavorazioneTotH { get; set; }
        public double? TempoLavorazioneTotMin { get; set; }
        public double? TempoLavorazioneH { get; set; }
        public double? TempoLavorazioneMin { get; set; }
        public double? TempoNonOperativoTotH { get; set; }
        public double? TempoNonOperativoTotMin { get; set; }
        public double? TempoNonOperativoH { get; set; }
        public double? TempoNonOperativoMin { get; set; }
        public double? TempoOperativoTotH { get; set; }
        public double? TempoOperativoTotMin { get; set; }
        public double? TempoOperativoH { get; set; }
        public double? TempoOperativoMin { get; set; }
        public double? TiroCentratore { get; set; }
        public double? VaporeInVascaBar { get; set; }
        public double? VaporeInVascaGradi { get; set; }
        public double? VaporeInVascaPercent { get; set; }
        public double? VelCilindroGommato01 { get; set; }
        public double? VelCilindroGommato02 { get; set; }
        public double? VelocitaCilindroDecatit { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Decofast35Datiesterni NumDisposizione { get; set; }
        public virtual ICollection<Decofast35Lavorazionemacchina> Decofast35Lavorazionemacchina { get; set; }
    }
}
