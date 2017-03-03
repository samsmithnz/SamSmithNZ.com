using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam.Models
{
 public   class PlayerGame
    {
        public int APICounter { get; set; }
        public long AppID { get; set; }
        public string GameName { get; set; }
        public long TotalMinutesPlayed { get; set; }
        public string TotalTimeString { get; set; }
        public string IconURL { get; set; }
        public string LogoURL { get; set; }
        public bool CommunityIsVisible { get; set; }
    }
}
