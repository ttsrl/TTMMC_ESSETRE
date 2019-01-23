using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using TTMMC.Utils;

namespace TTMMC.Models.DBModels
{
    public class Master
    {
        private string _name = "";
        private string _code = "";

        public int Id { get; set; }
        public string Name { get => _name; set => _name = value?.ToTrim().ToTitleCase(); }
        public string Code { get => _code; set => _code = value?.ToTrim(); }
        public int ColorArgb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }
        [NotMapped]
        public Color Color { get; set; }
    }
}
