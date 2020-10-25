using Microsoft.AspNetCore.Mvc.Rendering;
using SamSmithNZ.Service.Models.GuitarTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Models.GuitarTab
{
    public class TabsViewModel : BaseViewModel
    {
        public TabsViewModel(List<Rating> ratings, List<Tuning> tunings)
        {
            Ratings = new List<SelectListItem>();
            foreach (Rating item in ratings)
            {
                Ratings.Add(new SelectListItem(item.RatingCode.ToString(), item.RatingCode.ToString()));
            }
            Tunings = new List<SelectListItem>();
            foreach (Tuning item in tunings)
            {
                Tunings.Add(new SelectListItem(item.TuningName, item.TuningCode.ToString()));
            }
        }
        public Tab Tab { get; set; }
        public List<SelectListItem> Ratings { get; set; }
        public List<SelectListItem> Tunings { get; set; }
        public string Rating { get; set; }
        public string Tuning { get; set; }
    }
}
