using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Models
{
    public class PenaltyShootoutGoal
    {
        public PenaltyShootoutGoal() { }

        public int PenaltyCode { get; set; }
        public int GameCode { get; set; }
        public int PlayerCode { get; set; }
        public string  PlayerName { get; set; }
        public int PenaltyOrder { get; set; }
        public bool Scored { get; set; }

    }
}
