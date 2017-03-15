using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class FriendsDA
    {
        public List<Friend> GetFriends(string steamID)
        {
            SteamFriendDA da = new SteamFriendDA();
            SteamFriendList friendList = da.GetData(steamID);

            //Don't forget to add ME! :)
            string commaSeperatedSteamIDs = steamID.ToString();
            if (friendList != null)
            {
                foreach (SteamFriend item in friendList.friendslist.friends)
                {
                    commaSeperatedSteamIDs += "," + item.steamid.ToString();
                }
            }

            SteamPlayerDetailDA da2 = new SteamPlayerDetailDA();
            SteamPlayerDetail playerDetails = da2.GetData(commaSeperatedSteamIDs);

            List<Friend> processedFriendList = new List<Friend>();
            if (playerDetails != null)
            {
                foreach (SteamPlayer item in playerDetails.response.players)
                {
                    Friend friend = new Friend();
                    friend.SteamId = item.steamid;
                    friend.Name = item.personaname;
                    friend.LastLogoff = item.lastlogoff;
                    friend.ProfileURL = item.profileurl;
                    friend.Avatar = item.avatar;
                    friend.AvatarMedium = item.avatarmedium;
                    friend.AvatarFull = item.avatarfull;
                    friend.TimeCreated = item.timecreated;
                    friend.FriendSince = GetFriendSince(item.steamid, friendList.friendslist.friends);
                    processedFriendList.Add(friend);
                }
            }
            processedFriendList.Sort(delegate (Friend p1, Friend p2)
            {
                return p1.Name.CompareTo(p2.Name);
            });

            return processedFriendList;
        }

        public List<Friend> GetFriendsWithSameGame(string steamID, string appId)
        {
            SteamFriendDA da = new SteamFriendDA();
            SteamFriendList friendList = da.GetData(steamID);

            //Search my friends to see if they have the game we are searching for
            string commaSeperatedSteamIDs = "";
            if (friendList != null)
            {
                foreach (SteamFriend item in friendList.friendslist.friends)
                {
                    SteamOwnedGamesDA da3 = new SteamOwnedGamesDA();
                    SteamOwnedGames friendGames = da3.GetData(item.steamid);
                    if (friendGames.response.games != null)
                    {
                        foreach (Message item2 in friendGames.response.games)
                        {
                            if (item2.appid == appId)
                            {
                                commaSeperatedSteamIDs += "," + item.steamid.ToString();
                                break;
                            }
                        }
                    }
                }
            }

            //Get the friend details for this friend that has the right game
            SteamPlayerDetailDA da2 = new SteamPlayerDetailDA();
            SteamPlayerDetail playerDetails = da2.GetData(commaSeperatedSteamIDs);

            List<Friend> processedFriendList = new List<Friend>();
            if (playerDetails != null)
            {
                foreach (SteamPlayer item in playerDetails.response.players)
                {
                    Friend friend = new Friend();
                    friend.SteamId = item.steamid;
                    friend.Name = item.personaname;
                    friend.LastLogoff = item.lastlogoff;
                    friend.ProfileURL = item.profileurl;
                    friend.Avatar = item.avatar;
                    friend.AvatarMedium = item.avatarmedium;
                    friend.AvatarFull = item.avatarfull;
                    friend.TimeCreated = item.timecreated;
                    friend.FriendSince = GetFriendSince(item.steamid, friendList.friendslist.friends);
                    processedFriendList.Add(friend);
                }
            }
            processedFriendList.Sort(delegate (Friend p1, Friend p2)
            {
                return p1.Name.CompareTo(p2.Name);
            });

            return processedFriendList;

        }

        private long GetFriendSince(string steamID, List<SteamFriend> friends)
        {
            if (friends != null)
            {
                foreach (SteamFriend item in friends)
                {
                    if (item.steamid == steamID)
                    {
                        return item.friend_since;
                    }
                }
            }
            return 0;
        }

    }
}
