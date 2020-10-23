using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Models.Steam
{
    //<playerstats>
    //    <steamID>76561197971691578</steamID>
    //    <gameName>XCOM: Enemy Unknown</gameName>
    //    <achievements>
    //      <achievement>
    //          <apiname>ACHIEVEMENT_0</apiname>
    //          <achieved>1</achieved>
    //          <name>No Looking Back</name>
    //          <description>Beat the game in Ironman mode on Classic or Impossible Difficulty.</description>
    //      </achievement>
    //    </achievements>
    //    <success>true</success>
    //</playerstats>
    public class SteamPlayerAchievementsForApp
    {
        public PlayerStats playerstats { get; set; }
    }

    public class PlayerStats
    {
        public string steamID { get; set; }
        public string gameName { get; set; }
        public List<SteamPlayerAchievement> achievements { get; set; }
        public bool success { get; set; }
    }

    public class SteamPlayerAchievement
    {
        public string apiname { get; set; }
        public int achieved { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class SteamPlayerAchievementsForAppError
    {
        public PlayerStatsError playerstats { get; set; }
    }

    public class PlayerStatsError
    {
        public string error { get; set; }
        public bool success { get; set; }
    }
}
