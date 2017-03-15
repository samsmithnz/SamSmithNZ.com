using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    public class PlayerTest
    {

        [TestMethod]
        public void SamPlayerTest()
        {
            //Arrange
            PlayerDA da = new PlayerDA();
            string steamId = "76561197971691578";

            //Act
            Player result = da.GetData(steamId);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.SteamID == "76561197971691578");
            Assert.IsTrue(result.PlayerName == "Sam");
            Assert.IsTrue(result.IsPublic == true);
            //Assert.IsTrue(result.Games == null);
            //Assert.IsTrue(result.Games.Count >= 0);
            //Assert.IsTrue(result.Games[0].AppID == "");
            //Assert.IsTrue(result.Games[0].GameName == "");
            //Assert.IsTrue(result.Games[0].CommunityIsVisible == true);
            //Assert.IsTrue(result.Games[0].IconURL == "");
            //Assert.IsTrue(result.Games[0].LogoURL == "");
            //Assert.IsTrue(result.Games[0].TotalMinutesPlayed >= 0);
            //Assert.IsTrue(result.Games[0].TotalTimeString == "");
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
