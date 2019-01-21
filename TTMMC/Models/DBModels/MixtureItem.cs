using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMMC.Models.DBModels
{
    public class MixtureItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Material Material { get; set; }
    }
}
