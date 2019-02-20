namespace TTMMC_ESSETRE.Models.ViewModels
{

    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public int Code { get; set; }
    }
}
