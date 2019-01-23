using System.Collections.Generic;

namespace TTMMC.Models.ViewModels
{
    public class HomeModel
    {
        public List<IMachine> Machines { get; set; }
    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public int Code { get; set; }
    }
}
