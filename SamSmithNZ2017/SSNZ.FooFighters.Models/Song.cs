using System;

namespace SSNZ.FooFighters.Models
{
    public class Song
    {
        public int SongKey { get; set; }
        public string SongName { get; set; }
        public string SongNotes { get; set; }
        public string SongLyrics { get; set; }
        public string SongImage { get; set; }
        public DateTime FirstPlayed { get; set; }
        public int FirstPlayedShowKey { get; set; }
        public DateTime LastPlayed { get; set; }
        public int LastPlayedShowKey { get; set; }
        public int SongTimesPlayed { get; set; }
        public int AlbumKey { get; set; }
        public string AlbumName { get; set; }
        public int SongOrder { get; set; }


    }
}
