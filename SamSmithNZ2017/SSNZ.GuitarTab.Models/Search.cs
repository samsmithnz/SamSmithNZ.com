using System;
using System.Collections.Generic;

namespace SSNZ.GuitarTab.Models
{
    public class Search
    {
        public string SearchText { get; set; }
        public int AlbumCode { get; set; }
        public string ArtistAlbumResult { get; set; }
        public string TrackResult { get; set; }
        public string TrackName { get; set; }
        public bool IsBassTab { get; set; }

    }
}
