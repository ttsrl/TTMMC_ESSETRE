using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TTMMC.Models.ViewModels
{
    public class NewMouldModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Master { get; set; }
        public IFormFile Image { get; set; }
        public string Notes { get; set; }
    }
}
