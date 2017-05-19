using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.ITunes.Models
{
    public class Movement
    {
        public Movement() { }

        public String TrackName { get; set; }
        public short PlayCount { get; set; }
        public short ChangeThisMonth { get; set; }
    }
}



