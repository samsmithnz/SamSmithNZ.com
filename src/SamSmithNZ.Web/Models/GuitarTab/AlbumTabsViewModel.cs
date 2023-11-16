using SamSmithNZ.Service.Models.GuitarTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Models.GuitarTab
{
    public class AlbumTabsViewModel : BaseViewModel
    {
        public Album Album { get; set; }
        public List<Tab> Tabs { get; set; }
    }
}
