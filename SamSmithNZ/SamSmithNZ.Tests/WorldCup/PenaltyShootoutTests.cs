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
//    public class PenaltyShootoutTests : BaseIntegrationTest
//    {
//        [TestMethod]
//        public async Task PenaltyShootoutsExistTest()
//        {
//            //arrange
//            PenaltyShootoutGoalController controller = new PenaltyShootoutGoalController(new PenaltyShootoutGoalDataAccess(base.Configuration));
//           int gameCode = 7376;

//            //act
//            List<PenaltyShootoutGoal> results = await da.GetListAsync(gameCode);

//            //assert
//            Assert.IsTrue(results != null);
//            Assert.IsTrue(results.Count > 0);
//        }

//        [TestMethod()]
//        public async Task PenaltyShootoutsFirstItemTest()
//        {
//            //arrange
//            PenaltyShootoutGoalDataAccess da = new PenaltyShootoutGoalDataAccess();
//            int gameCode = 7376;

//            //act
//            List<PenaltyShootoutGoal> results = await da.GetListAsync(gameCode);

//            //assert
//            Assert.IsTrue(results != null);
//            Assert.IsTrue(results.Count > 0);
//            Assert.IsTrue(results[0].GameCode > 0);
//            Assert.IsTrue(results[0].PenaltyCode > 0);
//            Assert.IsTrue(results[0].PenaltyOrder == 1);
//            Assert.IsTrue(results[0].PlayerCode > 0);
//            Assert.IsTrue(results[0].PlayerName == "David Luiz");
//            Assert.IsTrue(results[0].Scored == true);
//            Assert.IsTrue(results[1].GameCode > 0);
//            Assert.IsTrue(results[1].PenaltyCode > 0);
//            Assert.IsTrue(results[1].PenaltyOrder == 2);
//            Assert.IsTrue(results[1].PlayerCode > 0);
//            Assert.IsTrue(results[1].PlayerName == "Mauricio Pinilla");
//            Assert.IsTrue(results[1].Scored == false);
//        }

//        [TestMethod()]
//        public async Task PenaltyShootoutsSaveItemTest()
//        {
//            //arrange
//            PenaltyShootoutGoalDataAccess da = new PenaltyShootoutGoalDataAccess();
//            int gameCode = 7376;

//            //act
//            List<PenaltyShootoutGoal> results = await da.GetListAsync(gameCode);
//            //Save the two goals
//            await da.SaveItemAsync(results[0]);
//            await da.SaveItemAsync(results[1]);
//            //Test that the results haven't changed
//            results = await da.GetListAsync(gameCode);

//            //assert
//            Assert.IsTrue(results != null);
//            Assert.IsTrue(results.Count > 0);
//            Assert.IsTrue(results[0].GameCode > 0);
//            Assert.IsTrue(results[0].PenaltyCode > 0);
//            Assert.IsTrue(results[0].PenaltyOrder == 1);
//            Assert.IsTrue(results[0].PlayerCode > 0);
//            Assert.IsTrue(results[0].PlayerName == "David Luiz");
//            Assert.IsTrue(results[0].Scored == true);
//            Assert.IsTrue(results[1].GameCode > 0);
//            Assert.IsTrue(results[1].PenaltyCode > 0);
//            Assert.IsTrue(results[1].PenaltyOrder == 2);
//            Assert.IsTrue(results[1].PlayerCode > 0);
//            Assert.IsTrue(results[1].PlayerName == "Mauricio Pinilla");
//            Assert.IsTrue(results[1].Scored == false);
//        }
//    }
//}
