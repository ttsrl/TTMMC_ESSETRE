using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTMMC_ESSETRE.Models.DBModels
{
    public class LayoutRecord
    {
        public int Id { get; set; }
        public List<LayoutRecordField> Fields { get; set; }
    }
}
