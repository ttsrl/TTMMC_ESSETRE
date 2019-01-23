using TTMMC.Utils;

namespace TTMMC.Models.DBModels
{
    public class Material
    {
        private string _name = "";
        private string _description = "";

        public int Id { get; set; }
        public string Name { get => _name; set => _name = value?.ToTrim().ToFirstCharUpper(); }
        public string Description { get => _description; set => _description = value?.ToTrim().ToFirstCharUpper(); }
    }
}
