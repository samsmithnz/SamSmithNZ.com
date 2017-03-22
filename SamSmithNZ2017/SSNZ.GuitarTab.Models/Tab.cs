using System;
using System.Collections.Generic;

namespace SSNZ.GuitarTab.Models
{
    public class Tab
    {
        public int TrackCode { get; set; }
        public int AlbumCode { get; set; }
        public string TrackName { get; set; }
        public string TrackText { get; set; }
        public int TrackOrder { get; set; }
        public int Rating { get; set; }
        public int TuningCode { get; set; }
        public string TuningName { get; set; }
        public DateTime LastUpdated { get; set; }
        
    }
}

