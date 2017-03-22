using System;
using System.Collections.Generic;

namespace SSNZ.GuitarTab.Models
{
    public class Search
    {
        public string SearchText { get; set; }
        public short AlbumCode { get; set; }
        public String ArtistAlbumResult { get; set; }
        public string TrackResult { get; set; }
        public String TrackName { get; set; }
        public Boolean IsBassTab { get; set; }

    }
}
