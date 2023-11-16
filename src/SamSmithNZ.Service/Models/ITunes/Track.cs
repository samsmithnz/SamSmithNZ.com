using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSmithNZ.Service.Models.ITunes
{
    public class Track
    {
        public Track() { }

        public int PlaylistCode { get; set; }
        public string TrackName { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public int PlayCount { get; set; }
        public int PreviousPlayCount { get; set; }
        public int Ranking { get; set; }
        public int PreviousRanking { get; set; }
        public bool IsNewEntry { get; set; }
        public int Rating { get; set; }
        public Guid RecordId { get; set; }
    }
}

