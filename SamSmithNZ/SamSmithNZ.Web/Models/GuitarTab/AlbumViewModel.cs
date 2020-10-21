using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.GuitarTab
{
    public class AlbumViewModel
    {
        public Album Album { get; set; }
        public List<Tab> Tabs { get; set; }
    }
}
