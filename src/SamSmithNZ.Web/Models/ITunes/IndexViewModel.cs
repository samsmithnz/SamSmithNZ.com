using SamSmithNZ.Service.Models.ITunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Models.ITunes
{
    public class IndexViewModel
    {
        public List<TopArtists> TopArtists { get; set; }
        public List<Playlist> Playlists { get; set; }
    }
}
