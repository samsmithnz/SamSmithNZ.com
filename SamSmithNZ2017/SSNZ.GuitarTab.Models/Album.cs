using System;
using System.Collections.Generic;

namespace SSNZ.GuitarTab.Models
{
    public class Album
    {
        public int AlbumCode { get; set; }
        public string ArtistName { get; set; }
        public string ArtistNameTrimed { get; set; }
        public bool IsLeadArtist { get; set; }
        public string AlbumName { get; set; }
        public int AlbumYear { get; set; }
        public int BassAlbumCode { get; set; }
        public bool IsBassTab { get; set; }
        public bool IsNewAlbum { get; set; }
        public bool IsMiscCollectionAlbum { get; set; }
        public bool IncludeInIndex { get; set; }
        public bool IncludeOnWebsite { get; set; }
        public decimal AverageRating { get; set; }

    }
}



