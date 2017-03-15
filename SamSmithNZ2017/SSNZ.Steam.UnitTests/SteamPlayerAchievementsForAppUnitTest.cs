using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SteamPlayerAchievementsForAppUnitTest
    {
        [TestMethod]
        public void PlayerAchievementsForAppExistUnitTest()
        {
            //Arrange
            string appId = "200510"; //XCOM
            string steamId = "76561197971691578";

            //Act
            SteamPlayerAchievementsForAppDA da = new SteamPlayerAchievementsForAppDA();
            SteamPlayerAchievementsForApp result = da.GetData(steamId, appId);

            //Asset
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.playerstats != null);
            Assert.IsTrue(result.playerstats.steamID == "76561197971691578");
            Assert.IsTrue(result.playerstats.gameName == "XCOM: Enemy Unknown");
            Assert.IsTrue(result.playerstats.achievements != null);
            Assert.IsTrue(result.playerstats.achievements.Count >= 1);
            Assert.IsTrue(result.playerstats.achievements[0].apiname == "ACHIEVEMENT_0");
            Assert.IsTrue(result.playerstats.achievements[0].achieved == 1);
            Assert.IsTrue(result.playerstats.achievements[0].name == "No Looking Back");
            Assert.IsTrue(result.playerstats.achievements[0].description == "Beat the game in Ironman mode on Classic or Impossible Difficulty.");
            Assert.IsTrue(result.playerstats.success == true);

            //<playerstats>
            //    <steamID>76561197971691578</steamID>
            //    <gameName>XCOM: Enemy Unknown</gameName>
            //    <achievements>
            //      <achievement>
            //          <apiname>ACHIEVEMENT_0</apiname>
            //          <achieved>1</achieved>
            //          <name>No Looking Back</name>
            //          <description>Beat the game in Ironman mode on Classic or Impossible Difficulty.</description>
            //      </achievement>
            //    </achievements>
            //    <success>true</success>
            //</playerstats>
        }
    }
}
