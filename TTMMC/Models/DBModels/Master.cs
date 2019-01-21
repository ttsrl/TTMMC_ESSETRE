using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace TTMMC.Models.DBModels
{
    public class Master
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ColorArgb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }
        [NotMapped]
        public Color Color { get; set; }
    }
}
