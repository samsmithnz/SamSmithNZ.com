using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam2019.Models
{
    //<game>
    //  <gameName>ValveTestApp200510</gameName>
    //  <gameVersion>67</gameVersion>
    //  <availableGameStats>
    //      <achievements>
    //          <achievement>
    //              <name>ACHIEVEMENT_0</name>
    //              <defaultvalue>0</defaultvalue>
    //              <displayName>No Looking Back</displayName>
    //              <hidden>0</hidden>
    //              <description>Beat the game in Ironman mode on Classic or Impossible Difficulty.</description>
    //              <icon>
    //              http://media.steampowered.com/steamcommunity/public/images/apps/200510/9ea8b01a66eb7af4d074430bbb3300414b7d457d.jpg
    //              </icon>
    //              <icongray>
    //              http://media.steampowered.com/steamcommunity/public/images/apps/200510/7dd89a5070fa8f8a07d9859c12a888a2885929ca.jpg
    //              </icongray>
    //          </achievement>
    //      </achievements>
    //  </availableGameStats>
    //</game>

    public class SteamGameDetail
    {
        public SteamGame game { get; set; }
    }

    public class SteamGame
    {
        public string gameName { get; set; }
        public int gameVersion { get; set; }
        public AvailableGameStats availableGameStats { get; set; }
    }

    public class AvailableGameStats
    {
        public List<GameAchievement> achievements { get; set; }
    }

    public class GameAchievement
    {
        public string name { get; set; }
        public int defaultvalue { get; set; }
        public string displayName { get; set; }
        public int hidden { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string icongray { get; set; }
    }
}
