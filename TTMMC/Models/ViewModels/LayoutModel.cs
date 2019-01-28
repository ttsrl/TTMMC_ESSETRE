using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TTMMC.Models.DBModels;

namespace TTMMC.Models.ViewModels
{
    public class IndexLayoutModel
    {
        public List<Layout> Layouts { get; set; }
        public List<IMachine> Machines { get; set; }
        public Dictionary<string, int> LogsCount { get; set; }
    }

    public class NewLayoutViewModel
    {
        public List<IMachine> Machines { get; set; }
        public List<Client> Clients { get; set; }
        public List<Mould> Moulds { get; set; }
        public List<Master> Masters { get; set; }
        public List<Mixture> Mixtures { get; set; }
        public Dictionary<int, string> Packaging { get; set; }
        public Client DefaultClient { get; set; }
        public Mould DefaultMould { get; set; }
        public Mixture DefaultMixture { get; set; }
    }

    public class NewLayoutModel
    {
        [Required]
        public int Client { get; set; }
        [Required]
        public int Mould { get; set; }
        [Required]
        public int Machine { get; set; }
        [Required]
        public int Master { get; set; }
        [Required]
        public int Mixture { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int MincedCheck { get; set; }
        public string Minced { get; set; }
        [Required]
        public int HumidifiedCheck { get; set; }
        public int Humidified { get; set; }
        [Required]
        public Package Packaging { get; set; }
        public int PackagingCount { get; set; }
        [Required]
        public DateTime Start { get; set; }
        public string Notes { get; set; }
    }
}
