using System.Collections.Generic;

namespace TTMMC_ESSETRE.Models.ViewModels
{
    public class IndexMachineModel
    {
        public List<IMachine> Machines { get; set; }
    }

    public class MachineDetailsModel
    {
        public IMachine Machine { get; set; }
    }
}