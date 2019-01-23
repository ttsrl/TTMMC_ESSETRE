using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TTMMC.Models.DBModels;
using TTMMC.Utils;

namespace TTMMC.Models.ViewModels
{
    public class IndexClientModel
    {
        public List<Client> Clients { get; set; }
    }

    public class NewClientModel
    {
        private string _address = "";
        private string _addressStreetMode = "";
        private string _addressNumber = "";

        [Required]
        public string Name { get; set; }
        [Required]
        public string VAT { get; set; }
        [Required]
        public string FiscalCode { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        public string AddressStreetMode { get => _addressStreetMode; set => _addressStreetMode = value?.ToTrim(); }
        [Required]
        public string Address { get => _address; set => _address = value?.ToTrim(); }
        [Required]
        public string AddressNumber { get => _addressNumber; set => _addressNumber = value?.ToTrim(); }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PEC { get; set; }
    }
}
