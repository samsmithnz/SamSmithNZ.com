using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerGamesTest
    {

        [TestMethod]
        public async Task PlayerGamesSamTest()
        {
            //Arrange
            PlayerGamesDA da = new PlayerGamesDA();
            string steamId = "76561197971691578";

            //Act
            List<Game> results = await da.GetDataAsync(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            Assert.IsTrue(results[0].AppID == "15100");
            Assert.IsTrue(results[0].GameName == "Assassin's Creed");
            Assert.IsTrue(results[0].CommunityIsVisible == false);
            Assert.IsTrue(results[0].IconURL == "cd8f7a795e34e16449f7ad8d8190dce521967917");
            Assert.IsTrue(results[0].LogoURL == "5450218e6f8ea246272cddcb2ab9a453b0ca7ef5");
            Assert.IsTrue(results[0].TotalMinutesPlayed == 185);
            Assert.IsTrue(results[0].TotalTimeString == "3 hrs");
        }

        [TestMethod]
        public async Task PlayerGamesSamWithNoIconTest()
        {
            //Arrange
            PlayerGamesDA da = new PlayerGamesDA();
            string steamId = "76561197971691578";
            string appId = "223530";

            //Act
            List<Game> results = await da.GetDataAsync(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            foreach (Game item in results)
            {
                if (item.AppID == appId)
                {
                    Assert.IsTrue(item.AppID == "223530");
                    Assert.IsTrue(item.GameName == "Left 4 Dead 2 Beta");
                    Assert.IsTrue(item.CommunityIsVisible == false);
                    Assert.IsTrue(item.IconURL == null);
                    Assert.IsTrue(item.LogoURL == "");
                    Assert.IsTrue(item.TotalMinutesPlayed == 0);
                    Assert.IsTrue(item.TotalTimeString == "0 hrs");
                    break;
                }
            }

        }

        [TestMethod]
        public async Task PlayerGamesRandomWithNoGamesTest()
        {
            //Arrange
            PlayerGamesDA da = new PlayerGamesDA();
            string steamId = "76561198059077520";

            //Act
            List<Game> results = await da.GetDataAsync(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 0);

        }

        [TestMethod]
        public async Task PlayerGamesRandomAnotherWithNoGamesTest()
        {
            //Arrange
            PlayerGamesDA da = new PlayerGamesDA();
            string steamId = "76561198121979762";

            //Act
            List<Game> results = await da.GetDataAsync(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 0);

        }
        
    }
}