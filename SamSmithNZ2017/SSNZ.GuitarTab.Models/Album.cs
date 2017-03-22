using System;
using System.Collections.Generic;

namespace SSNZ.GuitarTab.Models
{
    public class Album
    {
        public short AlbumCode { get; set; }
        public string ArtistName { get; set; }
        public string ArtistNameTrimed { get; set; }
        public string AlbumName { get; set; }
        public short AlbumYear { get; set; }
        public Boolean IsBassTab { get; set; }
        public Boolean IsNewAlbum { get; set; }
        public Boolean IsMiscCollectionAlbum { get; set; }
        public Boolean IncludeInIndex { get; set; }
        public Boolean IncludeOnWebsite { get; set; }
        public Decimal AverageRating { get; set; }

    }
}



