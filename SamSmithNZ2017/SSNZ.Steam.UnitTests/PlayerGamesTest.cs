using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;
using System.Collections.Generic;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerGamesTest
    {

        [TestMethod]
        public void SamPlayerGamesTest()
        {
            //Arrange
            PlayerGamesDA da = new PlayerGamesDA();
            string steamId = "76561197971691578";

            //Act
            List<Game> results = da.GetData(steamId);

            //Assert
            Assert.IsTrue(results!= null);
            Assert.IsTrue(results.Count >= 0);
            Assert.IsTrue(results[0].AppID == "15100");
            Assert.IsTrue(results[0].GameName == "Assassin's Creed");
            Assert.IsTrue(results[0].CommunityIsVisible == false);
            Assert.IsTrue(results[0].IconURL == "cd8f7a795e34e16449f7ad8d8190dce521967917");
            Assert.IsTrue(results[0].LogoURL == "5450218e6f8ea246272cddcb2ab9a453b0ca7ef5");
            Assert.IsTrue(results[0].TotalMinutesPlayed == 185);
            Assert.IsTrue(results[0].TotalTimeString == "3 hrs");
        }

        //[TestMethod]
        //public void SamSmithCastleStoryTest()
        //{
        //    //Arrange
        //    GameDetailsDA da = new GameDetailsDA();
        //    string steamId = "76561197971691578";
        //    string appId = "227860"; //castle Story

        //    //Act
        //    GameDetail result = da.GetData(steamId, appId);

        //    //Assert
        //    Assert.IsTrue(result != null);
        //    Assert.IsTrue(result.AppID == "227860");
        //    Assert.IsTrue(result.GameName == "Castle Story");
        //    Assert.IsTrue(result.IconURL == "5ba78b0a0b8197fcbef037b3ad0cc526fb5da4a1");
        //    Assert.IsTrue(result.LogoURL == "8456e045dd5f0311b71246c0c80b21b9b58c968e");
        //    Assert.IsTrue(result.PercentAchieved == 0m);
        //    Assert.IsTrue(result.TotalAchieved == 0m);
        //    Assert.IsTrue(result.Achievements.Count == 0);
        //}

        //[TestMethod]
        //public void SamSmithGodusTest()
        //{
        //    //Arrange
        //    GameDetailsDA da = new GameDetailsDA();
        //    string steamId = "76561197971691578";
        //    string appId = "232810"; //Godus

        //    //Act
        //    GameDetail result = da.GetData(steamId, appId);

        //    //Assert
        //    Assert.IsTrue(result != null);
        //    Assert.IsTrue(result.AppID == "232810");
        //    Assert.IsTrue(result.GameName == "Godus");
        //    Assert.IsTrue(result.IconURL == "4ee4e78811f8600fa39bc4377129b124b63e42a1");
        //    Assert.IsTrue(result.LogoURL == "e2a7637399293a7d2406157e6e4b833d519526ec");
        //    Assert.IsTrue(result.PercentAchieved == 0m);
        //    Assert.IsTrue(result.TotalAchieved == 0m);
        //    Assert.IsTrue(result.Achievements.Count == 0);
        //}

    }
}
