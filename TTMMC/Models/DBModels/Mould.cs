using System.Collections.Generic;
using TTMMC.Utils;

namespace TTMMC.Models.DBModels
{
    public class Mould
    {
        private string _description = "";
        private string _notes = "";
        private string _location = "";

        public int Id { get; set; }
        public string Code { get; set; }
        public Client DefaultClient { get; set; }
        public Mixture DefaultMixture { get; set; }
        public string Description { get => _description; set => _description = value?.ToFirstCharUpper(); }
        public string Location { get => _location; set => _location = value?.ToUpper(); }
        public string Image { get; set; }
        public string Notes { get => _notes; set => _notes = value?.ToFirstCharUpper(); }
    }

}
