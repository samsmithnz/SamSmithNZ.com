//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Threading.Tasks;
//using SamSmithnNZ.Tests;
//using SamSmithNZ.Service.Controllers.WorldCup;
//using SamSmithNZ.Service.DataAccess.WorldCup;
//using SamSmithNZ.Service.Models.WorldCup;
//using System.Collections.Generic;

//namespace SamSmithNZ.Tests.WorldCup
//{
//    [TestClass]
//    public class PlayerTests : BaseIntegrationTest
//    {
//        [TestMethod]
//        public async Task PlayersExistTest()
//        {
//            //arrange
//            PlayerController controller = new PlayerController(new PlayerDataAccess(base.Configuration));
//            int gameCode = 7328;

//            //act
//            List<Player> results = await da.GetListAsync(gameCode);

//            //assert
//            Assert.IsTrue(results != null);
//            Assert.IsTrue(results.Count > 0);
//        }

//        [TestMethod()]
//        public async Task PlayersFirstItemTest()
//        {
//            //arrange
//            PlayerDataAccess da = new PlayerDataAccess();
//            int gameCode = 7328;

//            //act
//            List<Player> results = await da.GetListAsync(gameCode);

//            //assert
//            Assert.IsTrue(results != null);
//            Assert.IsTrue(results.Count > 0);
//            Assert.IsTrue(results[0].Number ==2);
//            Assert.IsTrue(results[0].PlayerCode > 0);
//            Assert.IsTrue(results[0].PlayerName == "Alves, Dani (Brazil)");
//            Assert.IsTrue(results[0].Position == "DF");
//            Assert.IsTrue(results[0].TeamName == "Brazil");

//        }


//    }
//}
