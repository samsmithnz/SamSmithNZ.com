using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;
using System.Threading.Tasks;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerTest
    {

        [TestMethod]
        public async Task PlayerSamTest()
        {
            //Arrange
            PlayerDA da = new PlayerDA();
            string steamId = "76561197971691578";

            //Act
            Player result = await da.GetDataAsync(steamId);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.SteamID == "76561197971691578");
            Assert.IsTrue(result.PlayerName == "Sam");
            Assert.IsTrue(result.IsPublic == true);
        }


    }
}
