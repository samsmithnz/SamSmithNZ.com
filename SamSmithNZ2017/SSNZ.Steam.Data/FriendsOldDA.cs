using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class FriendsOldDA
    {
        public List<Friend> GetDataOld(string steamID)
        {
            //Get all friends
            SteamFriendDA da = new SteamFriendDA();
            SteamFriendList friendList = da.GetDataOld(steamID);

            //Don't forget to add the current user to the comma seperated list
            string commaSeperatedSteamIDs = steamID.ToString();

            //Build the comma seperated list for all friends
            if (friendList != null)
            {
                foreach (SteamFriend item in friendList.friendslist.friends)
                {
                    commaSeperatedSteamIDs += "," + item.steamid.ToString();
                }
            }

            //get the player details for all friends
            SteamPlayerDetailDA da2 = new SteamPlayerDetailDA();
            SteamPlayerDetail playerDetails = da2.GetDataOld(commaSeperatedSteamIDs);

            //Transfer the steam player details to the clean friend objects
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
                    if (friendList != null)
                    {
                        friend.FriendSince = GetFriendSince(item.steamid, friendList.friendslist.friends);
                    }
                    processedFriendList.Add(friend);
                }
            }

            //Sort the final list, as Steam returns the list by id, not name
            processedFriendList.Sort(delegate (Friend p1, Friend p2)
            {
                return p1.Name.CompareTo(p2.Name);
            });

            return processedFriendList;
        }

        public List<Friend> GetFriendsWithSameGameOld(string steamId, string appId)
        {
            //Get all friends
            SteamFriendDA da = new SteamFriendDA();
            SteamFriendList friendList = da.GetDataOld(steamId);

            //Search my friends to see if they have the game we are searching for
            string commaSeperatedSteamIDs = "";
            if (friendList != null)
            {
                foreach (SteamFriend item in friendList.friendslist.friends)
                {
                    SteamOwnedGamesDA da2 = new SteamOwnedGamesDA();
                    SteamOwnedGames friendGames = da2.GetDataOld(item.steamid);
                    if (friendGames != null && friendGames.response != null && friendGames.response.games != null)
                    {
                        foreach (Message item2 in friendGames.response.games)
                        {
                            //If my friends have the game, then yes! Add them to the comma delimited list to get more details later 
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
            SteamPlayerDetailDA da3 = new SteamPlayerDetailDA();
            SteamPlayerDetail playerDetails = da3.GetDataOld(commaSeperatedSteamIDs);

            //Transfer the steam player details to the clean friend objects
            List<Friend> processedFriendList = new List<Friend>();
            if (playerDetails != null)
            {
                foreach (SteamPlayer item in playerDetails.response.players)
                {
                    GameDetailsOldDA da4 = new GameDetailsOldDA();
                    Tuple<List<Achievement>, string> tempResults = da4.GetAchievementDataOld(item.steamid, appId, null);
                    if (tempResults.Item2 == null)
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
            }

            //Sort the final list, as Steam returns the list by id, not name
            processedFriendList.Sort(delegate (Friend p1, Friend p2)
            {
                return p1.Name.CompareTo(p2.Name);
            });

            return processedFriendList;

        }

        //Get the friend since information from the SteamFriend list
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
