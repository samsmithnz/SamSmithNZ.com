using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.GuitarTab
{
    public class IndexViewModel
    {
        public Album Album { get; set; }
        public List<Artist> Artists { get; set; }
    }
}
