using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam.Models;
using System.Net;

namespace SSNZ.Steam.Data
{
    public class SteamFriendDA
    {
        //http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&relationship=friend


        public RootObject GetData()
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&relationship=friend";
            string jsonData = new WebClient().DownloadString(jsonRequestString);

            RootObject root = JsonConvert.DeserializeObject<RootObject>(jsonData);
            return root;
        }
    }
}
