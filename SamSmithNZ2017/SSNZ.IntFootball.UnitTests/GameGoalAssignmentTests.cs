using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;

namespace SSNZ.IntFootball.UnitTests
{
    [TestClass]
    public class GameGoalAssignmentTests
    {
        [TestMethod]
        public async Task GameGoalAssignmentsExistTest()
        {
            //arrange
            GameGoalAssignmentDataAccess da = new GameGoalAssignmentDataAccess();
            int tournamentCode = 19;

            //act
            List<GameGoalAssignment> results = await da.GetListAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task GameGoalAssignmentsFirstItemTest()
        {
            //arrange
            GameGoalAssignmentDataAccess da = new GameGoalAssignmentDataAccess();
            int tournamentCode = 19;

            //act
            List<GameGoalAssignment> results = await da.GetListAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].GameCode > 0);
            Assert.IsTrue(results[0].GameNumber > 0);
            Assert.IsTrue(results[0].GameTime > DateTime.MinValue);
            Assert.IsTrue(results[0].Score != "");
            Assert.IsTrue(results[0].Team1Name != "");
            Assert.IsTrue(results[0].Team2Name != "");
            Assert.IsTrue(results[0].TotalGameTableGoals >= 0);
            Assert.IsTrue(results[0].TotalGameTablePenaltyShootoutGoals >= 0);
            Assert.IsTrue(results[0].TotalGoalTableGoals >= 0);
            Assert.IsTrue(results[0].TotalPenaltyShootoutTableGoals >= 0);        }
    }
}
