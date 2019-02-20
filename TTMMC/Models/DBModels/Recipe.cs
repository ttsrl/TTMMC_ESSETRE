using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTMMC_ESSETRE.Utils;

namespace TTMMC_ESSETRE.Models.DBModels
{
    public class Recipe
    {
        private DateTime? _timestamp;
        private string _name;

        public int Id { get; set; }
        public string Name { get => _name; set => _name = value?.ToTitleCase(); }
        public string Notes { get; set; }
        public LayoutRecord RepiceSettings { get; set; }
        public DateTime Timestamp
        {
            get => _timestamp ?? DateTime.Now;
            set => _timestamp = value;
        }
    }
}
