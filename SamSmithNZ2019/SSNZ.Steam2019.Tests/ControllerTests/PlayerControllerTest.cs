using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam2019.Service.Controllers;
using SSNZ.Steam2019.Service.Models;
using System.Threading.Tasks;

namespace SSNZ.Steam.UnitTests.ControllerTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerControllerTest
    {

        //Currently runs in 688ms
        [TestMethod]
        public async Task PlayerControllerSamTest()
        {
            //Arrange
            PlayerController controller = new PlayerController();
            string steamId = "76561197971691578";

            //Act
            Player result = await controller.GetPlayer(steamId);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.SteamID == "76561197971691578");
            Assert.IsTrue(result.PlayerName == "Sam");
            Assert.IsTrue(result.IsPublic == true);
        }


    }
}
