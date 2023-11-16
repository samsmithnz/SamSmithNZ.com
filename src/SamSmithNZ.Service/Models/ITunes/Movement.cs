using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSmithNZ.Service.Models.ITunes
{
    public class Movement
    {
        public Movement() { }

        public string TrackName { get; set; }
        public int PlayCount { get; set; }
        public int ChangeThisMonth { get; set; }
    }
}



