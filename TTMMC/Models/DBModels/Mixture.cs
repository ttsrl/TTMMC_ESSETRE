using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMMC.Models.DBModels
{
    public class Mixture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MixtureItem> Items { get; set; }
        public string Notes { get; set; }
    }
}
