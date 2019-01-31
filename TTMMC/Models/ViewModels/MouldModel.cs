using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TTMMC.Models.DBModels;

namespace TTMMC.Models.ViewModels
{
    public class IndexMouldModel
    {
        public List<Mould> Moulds { get; set; }
    }

    public class NewMouldViewModel
    {
        public List<Material> Materials { get; set; }
        public List<Mixture> Mixtures { get; set; }
        public List<Client> Clients { get; set; }
    }

    public class NewMouldModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public int Client { get; set; }
        [Required]
        public int Mixture { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        public IFormFile Image { get; set; }
        public string Notes { get; set; }

        public string MixtureName { get; set; }
        public Dictionary<string, int> MixtureQuantitys { get; set; }
        public Dictionary<string, int> MixtureMaterials { get; set; }
        public string MixtureNotes { get; set; }
    }

    public class EditMouldViewModel
    {
        public List<Mixture> Mixtures { get; set; }
        public List<Client> Clients { get; set; }
        public Mould Mould { get; set; }
    }
}
