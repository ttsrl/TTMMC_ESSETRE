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
        private string _phone = "";
        private string _email = "";
        private string _pec = "";

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get => _name; set => _name = value?.ToTrim().ToTitleCase(); }
        public string VAT { get => _vat; set => _vat = value?.ToTrim().ToUpper(); }
        public string FiscalCode { get => _fiscalCode; set => _fiscalCode = value?.ToTrim().ToUpper(); }
        public string Address { get => _address; set => _address = value?.ToTrim().ToTitleCase(); }
        public string Town { get => _town; set => _town = value?.ToTrim().ToTitleCase(); }
        public string Province { get => _province; set => _province = value?.ToTrim().ToUpper(); }
        public string State { get => _state; set => _state = value?.ToTrim().ToFirstCharUpper(); }
        public string Phone { get => _phone; set => _phone = value?.ToTrim(); }
        public string Email { get => _email; set => _email = value?.ToTrim().ToLower(); }
        public string PEC { get => _pec; set => _pec = value?.ToTrim().ToLower(); }
    }
}
