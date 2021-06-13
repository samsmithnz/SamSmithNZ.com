using Microsoft.AspNetCore.Mvc.Rendering;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.FooFighters
{
    public class ShowHistoryViewModel
    {
        public ShowHistoryViewModel(List<Year> years)
        {
            this.Years = new();
            this.Years.Add(new SelectListItem("<Select year>", "0"));
            foreach (Year item in years)
            {
                this.Years.Add(new SelectListItem(item.YearText, item.YearCode.ToString()));
            }
        }

        public int YearCode { get; set; }
        public List<SelectListItem> Years { get; set; }
        public List<AverageSetlist> AverageSetlists { get; set; }
        public List<Show> Shows { get; set; }
    }
}
