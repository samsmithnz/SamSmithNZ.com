using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSNZ.DevOps.Web.Models
{
    public class DevOpsMenuViewModel
    {
        public DevOpsMenuViewModel(bool mainHeader, bool section1, bool section2, bool section3, bool section4, bool section5, bool section6, bool section7)
        {
            MainHeader = mainHeader;
            Section1 = section1;
            Section2 = section2;
            Section3 = section3;
            Section4 = section4;
            Section5 = section5;
            Section6 = section6;
            Section7 = section7;
        }

        public bool MainHeader { get; set; }
        public bool Section1 { get; set; }
        public bool Section2 { get; set; }
        public bool Section3 { get; set; }
        public bool Section4 { get; set; }
        public bool Section5 { get; set; }
        public bool Section6 { get; set; }
        public bool Section7 { get; set; }
    }
}