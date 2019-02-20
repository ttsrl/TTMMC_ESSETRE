using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTMMC_ESSETRE.ConfigurationModels;
using TTMMC_ESSETRE.Models.DBModels;

namespace TTMMC_ESSETRE.Models.ViewModels
{
    public class IndexRecipeModel
    {
        public List<Recipe> Recipes { get; set; }
    }

    public class NewRecipeModel
    {
        public Dictionary<string, List<DataItem>> Fields { get; set; }
    }

    public class EditRecipeViewModel
    {
        public IMachine Machine { get; set; }
        public Recipe Recipe { get; set; }
    }
}
