using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class GroupCode
    {
        public GroupCode() { }

        public string RoundCode { get; set; }
        public bool IsLastRound { get; set; }
    }
}
