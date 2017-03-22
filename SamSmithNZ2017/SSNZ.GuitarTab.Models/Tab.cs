using System;
using System.Collections.Generic;

namespace SSNZ.GuitarTab.Models
{
    public class Tab
    {
        public short TrackCode { get; set; }
        public short AlbumCode { get; set; }
        public string TrackName { get; set; }
        public string TrackText { get; set; }
        public short TrackOrder { get; set; }
        public short Rating { get; set; }
        public short TuningCode { get; set; }
        public string TuningName { get; set; }
        public DateTime LastUpdated { get; set; }
        
    }
}

