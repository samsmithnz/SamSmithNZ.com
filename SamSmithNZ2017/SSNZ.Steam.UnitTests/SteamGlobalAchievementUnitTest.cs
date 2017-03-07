using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    public class SteamGlobalAchievementUnitTest
    {
        [TestMethod]
        public void GlobalAchievementUnitExistTest()
        {
            //Arrange
            string appId = "200510"; //XCOM

            //Act
            SteamGlobalAchievementPercentagesForAppDA da = new SteamGlobalAchievementPercentagesForAppDA();
            SteamGlobalAchievementsForApp result = da.GetData(appId);

            //Asset
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.achievementpercentages != null);
            Assert.IsTrue(result.achievementpercentages.achievements != null);
            Assert.IsTrue(result.achievementpercentages.achievements.Count >= 1);
            Assert.IsTrue(result.achievementpercentages.achievements[0].name == "ACHIEVEMENT_28");
            Assert.IsTrue(result.achievementpercentages.achievements[0].percent > 0m);
            Assert.IsTrue(result.achievementpercentages.achievements[0].percent < 100m);

            //<achievementpercentages> 
            //  <achievements> 
            //      <achievement> 
            //          <name>ACHIEVEMENT_28</name> 
            //          <percent>80.6781005859375</percent> 
            //      </achievement>
            //  <achievements>
            //<achievementpercentages>
        }
    }
}
