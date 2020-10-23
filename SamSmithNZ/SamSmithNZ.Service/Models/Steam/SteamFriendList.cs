using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Models.Steam
{
    //<friendslist> 
    //    <friends> 
    //        <friend> 
    //            <steamid>76561197960342203</steamid> 
    //            <relationship>friend</relationship> 
    //            <friend_since>1380999045</friend_since> 
    //        </friend>
    //    </friends> 
    //</friendslist>

    public class SteamFriendList
    {
        public Friendslist friendslist { get; set; }
    }

    public class Friendslist
    {
        public List<SteamFriend> friends { get; set; }
    }

    public class SteamFriend
    {
        public string steamid { get; set; }
        public string relationship { get; set; }
        public int friend_since { get; set; }
    }
}
