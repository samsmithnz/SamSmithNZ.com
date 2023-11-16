using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.GuitarTab
{
    public class IndexViewModel : BaseViewModel
    {
        public List<KeyValuePair<Artist, List<Album>>> ArtistAlbums { get; set; }
    }
}
