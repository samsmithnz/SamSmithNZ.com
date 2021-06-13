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
//    public class GameGoalAssignmentTests : BaseIntegrationTest
//    {
//        [TestMethod]
//        public async Task GameGoalAssignmentsExistTest()
//        {
//            //arrange
//            GameGoalAssignmentController controller = new GameGoalAssignmentController(new(base.Configuration));
//            int tournamentCode = 19;

//            //act
//            List<GameGoalAssignment> results = await da.GetListAsync(tournamentCode);

//            //assert
//            Assert.IsTrue(results != null);
//            Assert.IsTrue(results.Count > 0);
//        }

//        [TestMethod()]
//        public async Task GameGoalAssignmentsFirstItemTest()
//        {
//            //arrange
//            GameGoalAssignmentDataAccess da = new();
//            int tournamentCode = 19;

//            //act
//            List<GameGoalAssignment> results = await da.GetListAsync(tournamentCode);

//            //assert
//            Assert.IsTrue(results != null);
//            Assert.IsTrue(results.Count > 0);
//            Assert.IsTrue(results[0].GameCode > 0);
//            Assert.IsTrue(results[0].GameNumber > 0);
//            Assert.IsTrue(results[0].GameTime > DateTime.MinValue);
//            Assert.IsTrue(results[0].Score != "");
//            Assert.IsTrue(results[0].Team1Name != "");
//            Assert.IsTrue(results[0].Team2Name != "");
//            Assert.IsTrue(results[0].TotalGameTableGoals >= 0);
//            Assert.IsTrue(results[0].TotalGameTablePenaltyShootoutGoals >= 0);
//            Assert.IsTrue(results[0].TotalGoalTableGoals >= 0);
//            Assert.IsTrue(results[0].TotalPenaltyShootoutTableGoals >= 0);        }
//    }
//}
