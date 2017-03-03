using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    public class SteamOwnedGamesTest
    {
        [TestMethod]
        public void OwnedGamesExistTest()
        {
            //Arrange
            string steamId = "76561197971691578";

            //Act
            SteamOwnedGamesDA da = new SteamOwnedGamesDA();
            SteamOwnedGames result = da.GetData(steamId);

            //Asset
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.response.game_count >= 0);
            Assert.IsTrue(result.response.games != null);
            Assert.IsTrue(result.response.games.Count >= 1);
            Assert.IsTrue(result.response.games[0].appid == 220);
            Assert.IsTrue(result.response.games[0].name == "Half-Life 2");
            Assert.IsTrue(result.response.games[0].img_icon_url == "fcfb366051782b8ebf2aa297f3b746395858cb62");
            Assert.IsTrue(result.response.games[0].img_logo_url == "e4ad9cf1b7dc8475c1118625daf9abd4bdcbcad0");
            Assert.IsTrue(result.response.games[0].playtime_forever >= 3298);
            Assert.IsTrue(result.response.games[0].has_community_visible_stats == true);

        }
    }
}
