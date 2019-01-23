using System.Collections.Generic;
using TTMMC.Utils;

namespace TTMMC.Models.DBModels
{
    public class Mixture
    {
        private string _name = "";

        public int Id { get; set; }
        public string Name { get => _name; set => _name = value?.ToTrim().ToFirstCharUpper(); }
        public List<MixtureItem> Items { get; set; }
        public string Notes { get; set; }
    }
}
