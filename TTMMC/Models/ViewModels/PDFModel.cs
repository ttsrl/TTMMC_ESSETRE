using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTMMC.Models.DBModels;

namespace TTMMC.Models.ViewModels
{
    public class PDFMouldModuleModel
    {
        public Mould Mould { get; set; }
    }

    public class PDFClientModuleModel
    {
        public Client Client { get; set; }
    }

    public class PDFLayoutModuleModel
    {
        public Layout Layout { get; set; }
        public IMachine Machine { get; set; }
    }
}
