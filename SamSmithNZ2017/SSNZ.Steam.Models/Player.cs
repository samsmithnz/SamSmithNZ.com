using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam.Models
{
    public class Player
    {
        public string SteamID { get; set; }
        public string PlayerName { get; set; }
        public bool IsPublic { get; set; }
        public double SecondsToRunQuery { get; set; }
        public List<PlayerGame> Games { get; set; }
    }
}



