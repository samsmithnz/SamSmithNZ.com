//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SamSmithNZ.Service.DataAccess.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Tests.Steam
//{
//    [TestClass]
//    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
//    public class PlayerTest
//    {

//        [TestMethod]
//        public async Task PlayerSamTest()
//        {
//            //Arrange
//            PlayerDA da = new();
//            string steamId = "76561197971691578";

//            //Act
//            Player result = await da.GetDataAsync(steamId);

//            //Assert
//            Assert.IsTrue(result != null);
//            Assert.IsTrue(result.SteamID == "76561197971691578");
//            Assert.IsTrue(result.PlayerName == "Sam");
//            Assert.IsTrue(result.IsPublic == true);
//        }


//    }
//}
