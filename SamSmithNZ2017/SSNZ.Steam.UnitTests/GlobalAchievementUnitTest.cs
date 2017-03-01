using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    public class GlobalAchievementUnitTest
    {
        [TestMethod]
        public void GlobalAchievementUnitExistTest()
        {
            //Arrange
            string appId = "200510"; //XCOM

            //Act
            SteamGlobalAchievementPercentagesForAppDA da = new SteamGlobalAchievementPercentagesForAppDA();
            GlobalAchievementsForApp result = da.GetData(appId);

            //Asset
            Assert.IsTrue(result != null);
            //Assert.IsTrue(result.friendslist != null);
            //Assert.IsTrue(result.friendslist.friends != null);
            //Assert.IsTrue(result.friendslist.friends.Count >= 0);
            //Assert.IsTrue(result.friendslist.friends.Count >= 1);
            //Assert.IsTrue(result.friendslist.friends[0].steamid != "");
            //Assert.IsTrue(result.friendslist.friends[0].relationship != "");
            //Assert.IsTrue(result.friendslist.friends[0].friend_since >= 0);
        }
    }
}
