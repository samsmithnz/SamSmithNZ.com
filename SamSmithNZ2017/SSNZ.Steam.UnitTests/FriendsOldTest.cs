using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;
using System.Collections.Generic;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class FriendsOldTest
    {

        [TestMethod]
        public void SamFirstFriendOldTest()
        {
            //Arrange
            FriendsOldDA da = new FriendsOldDA();
            string steamId = "76561197971691578";

            //Act
            List<Friend> results = da.GetDataOld(steamId);

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

        [TestMethod]
        public void AlexFriendsOldTest()
        {
            //Arrange
            FriendsOldDA da = new FriendsOldDA();
            string steamId = "76561198034342716";

            //Act
            List<Friend> results = da.GetDataOld(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            //Friend result = null;
            //foreach (Friend item in results)
            //{
            //    if (item.SteamId == "76561197971691578") //Sam
            //    {
            //        result = item;
            //        break;
            //    }
            //}
            //Assert.IsTrue(result != null);
            //Assert.IsTrue(result.SteamId == "76561197971691578");
            //Assert.IsTrue(result.Name == "Sam");
        }

        [TestMethod]
        public void SamXcomFriendsOldTest()
        {
            //Arrange
            FriendsOldDA da = new FriendsOldDA();
            string steamId = "76561197971691578";
            string appId = "200510"; //Xcom

            //Act
            List<Friend> results = da.GetFriendsWithSameGameOld(steamId, appId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            Friend result = null;
            foreach (Friend item in results)
            {
                if (item.SteamId == "76561197990013217") //Stew
                {
                    result = item;
                    break;
                }
            }
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.SteamId == "76561197990013217");
            Assert.IsTrue(result.Name == "Captain Datsun");

        }

        [TestMethod]
        public void SamCiv6FriendsOldTest()
        {
            //Arrange
            FriendsOldDA da = new FriendsOldDA();
            string steamId = "76561197971691578";
            string appId = "289070"; //Civ 6

            //Act
            List<Friend> results = da.GetFriendsWithSameGameOld(steamId, appId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 1);
            Friend result = null;
            foreach (Friend item in results)
            {
                if (item.SteamId == "76561198030842184") //Coleman
                {
                    result = item;
                }
                else if (item.SteamId == "76561198034342716") //Alex
                {
                    Assert.IsTrue(false); //This should never happen, Alex has a private profile
                }
            }
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.SteamId == "76561198030842184");
            Assert.IsTrue(result.Name == "coleman.yeaw");

        }

        [TestMethod]
        public void StewXcomFriendsOldTest()
        {
            //Arrange
            FriendsOldDA da = new FriendsOldDA();
            string steamId = "76561197990013217";
            string appId = "200510"; //Xcom

            //Act
            List<Friend> results = da.GetFriendsWithSameGameOld(steamId, appId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            Friend result = null;
            foreach (Friend item in results)
            {
                if (item.SteamId == "76561197971691578") //Sam
                {
                    result = item;
                    break;
                }
            }
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.SteamId == "76561197971691578");
            Assert.IsTrue(result.Name == "Sam");
        }

        [TestMethod]
        public void RandomFriendsOldTest()
        {
            //Arrange
            FriendsOldDA da = new FriendsOldDA();
            string steamId = "76561198154034472";

            //Act
            List<Friend> results = da.GetDataOld(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
        }

    }
}
