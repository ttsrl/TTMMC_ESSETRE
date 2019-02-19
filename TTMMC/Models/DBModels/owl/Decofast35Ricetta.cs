using System;
using System.Collections.Generic;

namespace TTMMC_ESSETRE.Models
{
    public partial class Decofast35Ricetta
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string NomeRicetta { get; set; }
        public double? SetAspirazioneBocchette { get; set; }
        public double? SetContametri { get; set; }
        public double? SetCorrezioneNastroRaffred { get; set; }
        public double? SetRiscaldCilindrDecatitore { get; set; }
        public double? SetRulloLucidante { get; set; }
        public double? SetTensElettronicaFeltro { get; set; }
        public double? SetTensPneumaticaFeltro { get; set; }
        public double? SetTiroBallerCilindrAlimen { get; set; }
        public double? SetVaporeInVasca { get; set; }
        public double? SetVelMacchina { get; set; }
    }
}
