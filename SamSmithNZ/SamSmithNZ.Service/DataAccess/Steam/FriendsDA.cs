using SamSmithNZ.Service.Models.Steam;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class FriendsDA
    {
        public async Task<List<Friend>> GetDataAsync(string steamID)
        {
            List<Friend> processedFriendList = new();

            //Get all friends
            SteamFriendDA da = new();
            SteamFriendList friendList = await da.GetDataAsync(steamID);

            //Don't forget to add the current user to the comma seperated list
            string commaSeperatedSteamIDs = steamID.ToString();
            List<string> commaSeperatedSteamIDsArray = new();

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
                SteamPlayerDetailDA da2 = new();
                SteamPlayerDetail playerDetails = await da2.GetDataAsync(CommaSeperatedItem);

                //Transfer the steam player details to the clean friend objects
                if (playerDetails != null)
                {
                    foreach (SteamPlayer item in playerDetails.response.players)
                    {
                        Friend friend = new()
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

        //Get the friend since information from the SteamFriend list
        private static long GetFriendSince(string steamID, List<SteamFriend> friends)
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
