﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;
using System.Threading.Tasks;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SteamGlobalAchievementUnitTest
    {
        [TestMethod]
        public async Task SteamGlobalAchievementUnitExistTest()
        {
            //Arrange
            string appId = "200510"; //XCOM

            //Act
            SteamGlobalAchievementPercentagesForAppDA da = new SteamGlobalAchievementPercentagesForAppDA();
            SteamGlobalAchievementsForApp result = await da.GetDataAsync(appId);

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
