using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class FriendsListDetail
    {
        public FriendsListDetail(string steamid, string personaname, long lastlogoff, string profileurl, string avatar, string avatarmedium, string avatarfull, long timecreated, long friend_since)
        {
            this.Steamid = steamid;
            this.Name = personaname;
            this.LastLogoff = lastlogoff;
            this.ProfileURL = profileurl;
            this.Avatar = avatar;
            this.AvatarMedium = avatarmedium;
            this.AvatarFull = avatarfull;
            this.TimeCreated = timecreated;
            this.FriendSince = friend_since;
        }

        public string Steamid { get; set; }
        public string Name { get; set; }
        public long LastLogoff { get; set; }
        public string ProfileURL { get; set; }
        public string Avatar { get; set; }
        public string AvatarMedium { get; set; }
        public string AvatarFull { get; set; }
        public long TimeCreated { get; set; }
        public long FriendSince { get; set; }
        //public List<OwnedGames> { get; set; }

        public static List<FriendsListDetail> LoadFriendsList(string steamID)
        {
            SteamFriendDA da = new SteamFriendDA();
            FriendList friendList = da.GetData(steamID);

            //Don't forget to add ME! :)
            string commaSeperatedSteamIDs = steamID.ToString();
            if (friendList != null)
            {
                foreach (Friend item in friendList.friendslist.friends)
                {
                    commaSeperatedSteamIDs += "," + item.steamid.ToString();
                }
            }

            SteamPlayerDetailDA da2 = new SteamPlayerDetailDA();
            PlayerDetail playerDetails = da2.GetData(commaSeperatedSteamIDs);

            List<FriendsListDetail> processedFriendList = new List<FriendsListDetail>();
            if (playerDetails != null)
            {
                foreach (Player item in playerDetails.response.players)
                {
                    long friendSince;
                    if (friendList != null)
                    {
                        friendSince = FriendsListDetail.GetFriendSince(item.steamid, friendList.friendslist.friends);
                    }
                    else
                    {
                        friendSince = 0;
                    }
                    FriendsListDetail friend = new FriendsListDetail(item.steamid, item.personaname, item.lastlogoff, item.profileurl, item.avatar, item.avatarmedium, item.avatarfull, item.timecreated, friendSince);
                    processedFriendList.Add(friend);
                }
            }
            processedFriendList.Sort(delegate (FriendsListDetail p1, FriendsListDetail p2)
            {
                return p1.Name.CompareTo(p2.Name);
            });

            return processedFriendList;
        }

        private static long GetFriendSince(string steamID, List<Friend> friends)
        {
            foreach (Friend item in friends)
            {
                if (item.steamid == steamID)
                {
                    return item.friend_since;
                }
            }
            return 0;
        }

    }
}
