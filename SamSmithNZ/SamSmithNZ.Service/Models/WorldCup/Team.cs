using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class Team
    {
        public Team() { }

        public int TeamCode { get; set; }
        public string TeamName { get; set; }
        public string FlagName { get; set; }

    }
}
