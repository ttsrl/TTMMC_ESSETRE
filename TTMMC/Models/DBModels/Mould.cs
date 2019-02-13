using System.Collections.Generic;
using TTMMC.Utils;

namespace TTMMC.Models.DBModels
{
    public class Mould
    {
        private string _code = "";
        private string _description = "";
        private string _location = "";

        public int Id { get; set; }
        public string Code { get => _code; set => _code = value?.ToTrim(); }
        public Client DefaultClient { get; set; }
        public Mixture DefaultMixture { get; set; }
        public Master DefaultMaster { get; set; }
        public string Description { get => _description; set => _description = value?.ToTrim().ToFirstCharUpper(); }
        public string Location { get => _location; set => _location = value?.ToTrim().ToUpper(); }
        public string Image { get; set; }
        public string Notes { get; set; }
    }

}
