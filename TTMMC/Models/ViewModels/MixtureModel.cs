using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTMMC.Models.DBModels;

namespace TTMMC.Models.ViewModels
{
    public class IndexMixtureModel
    {
        public List<Material> Materials { get; set; }
        public List<Mixture> Mixtures { get; set; }
    }
}
