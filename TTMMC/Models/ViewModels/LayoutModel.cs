using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TTMMC_ESSETRE.Models.DBModels;

namespace TTMMC_ESSETRE.Models.ViewModels
{
    public class IndexLayoutModel
    {
        public List<Layout> Layouts { get; set; }
        public List<IMachine> Machines { get; set; }
    }

}
