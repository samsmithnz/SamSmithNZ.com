using SamSmithNZ.Service.Models.ITunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Models.ITunes
{
    public class PlaylistViewModel
    {
        public Playlist Playlist { get; set; }
        public List<TopArtists> TopArtists { get; set; }
        public List<Movement> Movements { get; set; }
        public List<Track> Tracks { get; set; }
    }
}
