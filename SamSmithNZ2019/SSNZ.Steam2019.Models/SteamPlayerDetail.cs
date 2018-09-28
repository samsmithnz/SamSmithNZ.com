using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam2019.Models
{
    //<response> 
    //    <players> 
    //        <player> 
    //            <steamid>76561197974118008</steamid> 
    //            <communityvisibilitystate>3</communityvisibilitystate> 
    //            <profilestate>1</profilestate> 
    //            <personaname>nosilleg</personaname> 
    //            <lastlogoff>1392847930</lastlogoff> 
    //            <commentpermission>2</commentpermission> 
    //            <profileurl>http://steamcommunity.com/id/nosilleg/</profileurl> 
    //            <avatar>http://media.steampowered.com/steamcommunity/public/images/avatars/85/8522d758971df1d6c4046cca81e8a364315388f2.jpg</avatar> 
    //            <avatarmedium>http://media.steampowered.com/steamcommunity/public/images/avatars/85/8522d758971df1d6c4046cca81e8a364315388f2_medium.jpg</avatarmedium> 
    //            <avatarfull>http://media.steampowered.com/steamcommunity/public/images/avatars/85/8522d758971df1d6c4046cca81e8a364315388f2_full.jpg</avatarfull> 
    //            <personastate>0</personastate> 
    //            <primaryclanid>103582791433470748</primaryclanid> 
    //            <timecreated>1108190390</timecreated> 
    //            <personastateflags>0</personastateflags> 
    //            <loccountrycode>GB</loccountrycode> 
    //            <locstatecode>50</locstatecode>
    //        </player>
    //    </playesr>
    //</response>

    public class SteamPlayerDetail
    {
        public PlayerResponse response { get; set; }
    }

    public class PlayerResponse
    {
        public List<SteamPlayer> players { get; set; }
    }

    public class SteamPlayer
    {
        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public int lastlogoff { get; set; }
        public int commentpermission { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public int personastate { get; set; }
        public string primaryclanid { get; set; }
        public int timecreated { get; set; }
        public int personastateflags { get; set; }
    }
}
