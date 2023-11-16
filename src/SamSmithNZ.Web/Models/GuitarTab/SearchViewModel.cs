using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.GuitarTab
{
    public class SearchViewModel : BaseViewModel
    {
        public List<Search> SearchResults { get; set; }
    }
}
