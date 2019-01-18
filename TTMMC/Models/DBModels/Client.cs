using TTMMC.Utils;

namespace TTMMC.Models.DBModels
{
    public class Client
    {
        private string _name = "";
        private string _vat = "";
        private string _fiscalCode = "";
        private string _address = "";
        private string _town = "";
        private string _province = "";
        private string _state = "";
        private string _email = "";
        private string _pec = "";

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get => _name; set => _name = value?.ToTitleCase(); }
        public string VAT { get => _vat; set => _vat = value?.ToUpper(); }
        public string FiscalCode { get => _fiscalCode; set => _fiscalCode = value?.ToUpper(); }
        public string Address { get => _address; set => _address = value?.ToTitleCase(); }
        public string Town { get => _town; set => _town = value?.ToTitleCase(); }
        public string Province { get => _province; set => _province = value?.ToUpper(); }
        public string State { get => _state; set => _state = value?.ToFirstCharUpper(); }
        public string Phone { get; set; }
        public string Email { get => _email; set => _email = value?.ToLower(); }
        public string PEC { get => _pec; set => _pec = value?.ToLower(); }
    }
}
