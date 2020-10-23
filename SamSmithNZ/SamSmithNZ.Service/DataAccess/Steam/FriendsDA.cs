using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.Steam.Interfaces;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class FriendsDA
    {
        public async Task<List<Friend>> GetDataAsync(IRedisService redisService, string steamID, bool useCache)
        {
            List<Friend> processedFriendList = new List<Friend>();

            //Get all friends
            SteamFriendDA da = new SteamFriendDA();
            SteamFriendList friendList = await da.GetDataAsync(redisService, steamID, useCache);

            //Don't forget to add the current user to the comma seperated list
            string commaSeperatedSteamIDs = steamID.ToString();
            List<string> commaSeperatedSteamIDsArray = new List<string>();

            //Build the comma seperated list for all friends
            if (friendList != null && friendList.friendslist != null)
            {
                int friendsLength = friendList.friendslist.friends.Count + 1;
                if (friendsLength <= 100)
                {
                    foreach (SteamFriend item in friendList.friendslist.friends)
                    {
                        commaSeperatedSteamIDs += "," + item.steamid.ToString();
                    }
                }
                else
                {
                    int playerDetails100FriendSplitArrayLength = friendList.friendslist.friends.Count / 100;
                    int playerDetails100FriendSplitLengthDifference = friendList.friendslist.friends.Count - (playerDetails100FriendSplitArrayLength * 100);
                    if (playerDetails100FriendSplitLengthDifference > 0)
                    {
                        playerDetails100FriendSplitArrayLength++;
                    }

                    for (int arrayCount = 0; arrayCount < playerDetails100FriendSplitArrayLength; arrayCount++)
                    {
                        for (int itemCount = 0; itemCount < 100; itemCount++)
                        {
                            if (itemCount == 0)
                            {
                                if (arrayCount == 0)
                                {
                                    commaSeperatedSteamIDs = steamID.ToString();
                                }
                                else
                                {
                                    commaSeperatedSteamIDs += friendList.friendslist.friends[arrayCount + itemCount].steamid.ToString();
                                }
                            }
                            else
                            {
                                commaSeperatedSteamIDs += "," + friendList.friendslist.friends[arrayCount + itemCount].steamid.ToString();
                            }
                            if (itemCount == 99)
                            {
                                commaSeperatedSteamIDsArray.Add(commaSeperatedSteamIDs);
                                commaSeperatedSteamIDs = "";
                            }
                        }
                    }
                }
                commaSeperatedSteamIDsArray.Add(commaSeperatedSteamIDs);
            }

            foreach (string CommaSeperatedItem in commaSeperatedSteamIDsArray)
            {
                //get the player details for all friends
                SteamPlayerDetailDA da2 = new SteamPlayerDetailDA();
                SteamPlayerDetail playerDetails = await da2.GetDataAsync(redisService, CommaSeperatedItem, useCache);

                //Transfer the steam player details to the clean friend objects
                if (playerDetails != null)
                {
                    foreach (SteamPlayer item in playerDetails.response.players)
                    {
                        Friend friend = new Friend
                        {
                            SteamId = item.steamid,
                            Name = item.personaname,
                            LastLogoff = item.lastlogoff,
                            ProfileURL = item.profileurl,
                            Avatar = item.avatar,
                            AvatarMedium = item.avatarmedium,
                            AvatarFull = item.avatarfull,
                            TimeCreated = item.timecreated
                        };
                        if (friendList != null)
                        {
                            friend.FriendSince = GetFriendSince(item.steamid, friendList.friendslist.friends);
                        }
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


        //public async Task<List<Friend>> GetFriendsWithSameGame(IRedisService redisService, string steamId, string appId, bool useCache)
        //{
        //    //Get all friends
        //    SteamFriendDA da = new SteamFriendDA();
        //    SteamFriendList friendList = await da.GetDataAsync(redisService, steamId, useCache);

        //    //Search my friends to see if they have the game we are searching for
        //    string commaSeperatedSteamIDs = "";
        //    int i = 0;
        //    if (friendList != null && friendList.friendslist != null)
        //    {
        //        foreach (SteamFriend item in friendList.friendslist.friends)
        //        {
        //            if (i >= 100) //This steam function accepts a maximum of 100 steam id's
        //            {
        //                break;
        //            }
        //            SteamOwnedGamesDA da2 = new SteamOwnedGamesDA();
        //            SteamOwnedGames friendGames = await da2.GetDataAsync(redisService, item.steamid, useCache);
        //            if (friendGames != null && friendGames.response != null && friendGames.response.games != null)
        //            {
        //                foreach (Message item2 in friendGames.response.games)
        //                {
        //                    //If my friends have the game, then yes! Add them to the comma delimited list to get more details later 
        //                    if (item2.appid == appId)
        //                    {
        //                        i++;
        //                        commaSeperatedSteamIDs += "," + item.steamid.ToString();
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    //Get the friend details for this friend that has the right game
        //    SteamPlayerDetailDA da3 = new SteamPlayerDetailDA();
        //    SteamPlayerDetail playerDetails = await da3.GetDataAsync(redisService, commaSeperatedSteamIDs, useCache);

        //    //Transfer the steam player details to the clean friend objects
        //    List<Friend> processedFriendList = new List<Friend>();
        //    if (playerDetails != null)
        //    {
        //        foreach (SteamPlayer item in playerDetails.response.players)
        //        {
        //            GameDetailsDA da4 = new GameDetailsDA();
        //            Tuple<List<Achievement>, string> tempResults = await da4.GetAchievementDataAsync(redisService, item.steamid, appId, null, null, useCache);
        //            if (tempResults.Item2 == null)
        //            {
        //                Friend friend = new Friend
        //                {
        //                    SteamId = item.steamid,
        //                    Name = item.personaname,
        //                    LastLogoff = item.lastlogoff,
        //                    ProfileURL = item.profileurl,
        //                    Avatar = item.avatar,
        //                    AvatarMedium = item.avatarmedium,
        //                    AvatarFull = item.avatarfull,
        //                    TimeCreated = item.timecreated,
        //                    FriendSince = GetFriendSince(item.steamid, friendList.friendslist.friends)
        //                };
        //                processedFriendList.Add(friend);
        //            }
        //        }
        //    }

        //    //Sort the final list, as Steam returns the list by id, not name
        //    processedFriendList.Sort(delegate (Friend p1, Friend p2)
        //    {
        //        return p1.Name.CompareTo(p2.Name);
        //    });

        //    return processedFriendList;

        //}

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
