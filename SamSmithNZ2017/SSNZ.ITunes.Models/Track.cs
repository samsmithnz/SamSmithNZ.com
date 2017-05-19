using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.ITunes.Models
{
    public class Track
    {
        public Track() { }

        public int PlaylistCode { get; set; }
        public string TrackName { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public short PlayCount { get; set; }
        public short PreviousPlayCount { get; set; }
        public short Ranking { get; set; }
        public short PreviousRanking { get; set; }
        public bool IsNewEntry { get; set; }
        public short Rating { get; set; }
        public Guid RecordId { get; set; }
    }
}

