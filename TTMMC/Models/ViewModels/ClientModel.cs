using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TTMMC.Models.DBModels;

namespace TTMMC.Models.ViewModels
{
    public class IndexClientModel
    {
        public List<Client> Clients { get; set; }
    }

    public class NewClientModel
    {
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
        public string AddressStreetMode { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AddressNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PEC { get; set; }
    }
}
