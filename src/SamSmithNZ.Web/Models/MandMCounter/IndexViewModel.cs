using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Models.MandMCounter
{
    public class IndexViewModel
    {
        public IndexViewModel(List<string> unitsForVolume, List<string> unitsForContainer)
        {
            UnitsForVolume = new List<SelectListItem>();
            foreach (string item in unitsForVolume)
            {
                UnitsForVolume.Add(new SelectListItem(item, item));
            }
            UnitsForContainer = new List<SelectListItem>();
            foreach (string item in unitsForContainer)
            {
                UnitsForContainer.Add(new SelectListItem(item, item));
            }
        }

        public List<SelectListItem> UnitsForVolume { get; set; }
        public List<SelectListItem> UnitsForContainer { get; set; }
        public string VolumeUnit { get; set; }
        public string ContainerUnit { get; set; }
        public float Quantity { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Length { get; set; }
        public float Radius { get; set; }
        public float MandMResult { get; set; }
        public float PeanutMandMResult { get; set; }
        public float SkittlesResult { get; set; }
        public string ActiveTab { get; set; }
    }
}
