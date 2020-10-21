using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class Player
    {
        public Player() { }

        public int PlayerCode { get; set; }
        public string  PlayerName { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
        public string TeamName { get; set; }

    }
}
