using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam2019.Models
{
    //<response>
    //    <game_count>74</game_count>
    //    <games>
    //      <message>
    //          <appid>220</appid>
    //          <name>Half-Life 2</name>
    //          <playtime_forever>3298</playtime_forever>
    //          <img_icon_url>fcfb366051782b8ebf2aa297f3b746395858cb62</img_icon_url>
    //          <img_logo_url>e4ad9cf1b7dc8475c1118625daf9abd4bdcbcad0</img_logo_url>
    //          <has_community_visible_stats>true</has_community_visible_stats>
    //      </message>
    //    </games>
    //</response>
    public class SteamOwnedGames
    {
        public OwnedGamesResponse response { get; set; }
    }

    public class OwnedGamesResponse
    {
        public int game_count { get; set; }
        public List<Message> games { get; set; }
    }

    public class Message
    {
        public string appid { get; set; }
        public string name { get; set; }
        public int playtime_forever { get; set; }
        public string img_icon_url { get; set; }
        public string img_logo_url { get; set; }
        public bool has_community_visible_stats { get; set; }
    }
}
