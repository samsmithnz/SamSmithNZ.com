using System;

namespace SSNZ.Lego.Models
{
    public class LegoOwnedSets
    {
        public string SetNum { get; set; }
        public string SetName { get; set; }
        public int SetYear { get; set; }
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
        public int NumberOfParts { get; set; }
        public int InventoryId { get; set; }
    }
}
