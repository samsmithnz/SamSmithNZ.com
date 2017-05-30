using System;

namespace SSNZ.FooFighters.Models
{
    public class Song
    {
        public int SongCode { get; set; }
        public string SongName { get; set; }
        public string SongNotes { get; set; }
        public string SongLyrics { get; set; }
        public string SongImage { get; set; }
        public DateTime? FirstPlayed { get; set; }
        public int? FirstPlayedShowCode { get; set; }
        public DateTime? LastPlayed { get; set; }
        public int? LastPlayedShowCode { get; set; }
        public int TimesPlayed { get; set; }
        public int AlbumCode { get; set; }
        public string AlbumName { get; set; }
        public int SongOrder { get; set; }


    }
}
