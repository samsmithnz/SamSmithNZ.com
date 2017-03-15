using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;
using System.Collections.Generic;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    public class FriendsTest
    {

        [TestMethod]
        public void SamFirstFriendTest()
        {
            //Arrange
            FriendsDA da = new FriendsDA();
            string steamId = "76561197971691578";

            //Act
            List<Friend> results = da.GetFriends(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            Friend result = null;
            foreach (Friend item in results)
            {
                if (item.SteamId == "76561198034342716")
                {
                    result = item;
                    break;
                }
            }
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.SteamId == "76561198034342716");
            Assert.IsTrue(result.Name == "Alex");
            Assert.IsTrue(result.Avatar == "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/c5/c5e94b1718c9e14d6d17be6c2c5b0fe41f5eb12e.jpg");
            Assert.IsTrue(result.AvatarFull == "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/c5/c5e94b1718c9e14d6d17be6c2c5b0fe41f5eb12e_full.jpg");
            Assert.IsTrue(result.AvatarMedium == "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/c5/c5e94b1718c9e14d6d17be6c2c5b0fe41f5eb12e_medium.jpg");
            Assert.IsTrue(result.FriendSince >= 0);
            Assert.IsTrue(result.LastLogoff >= 0);
            Assert.IsTrue(result.ProfileURL == "http://steamcommunity.com/profiles/76561198034342716/");
            Assert.IsTrue(result.TimeCreated >= 0);
        }
        
    }
}
