using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;

namespace SSNZ.IntFootball.UnitTests
{
    [TestClass]
    public class TournamentTopGoalScorerTests
    {
        [TestMethod]
        public async Task TournamentTopGoalScorerTest()
        {
            //arrange
            TournamentTopGoalScorerDataAccess da = new TournamentTopGoalScorerDataAccess();
            int tournamentCode = 21;

            //act
            List<TournamentTopGoalScorer> results = await da.GetListAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].PlayerName == "Harry Kane");
            Assert.IsTrue(results[0].FlagName != "");
            Assert.IsTrue(results[0].GoalsScored > 0);
            Assert.IsTrue(results[0].IsActive == false);
            Assert.IsTrue(results[0].TeamCode == 10);
            Assert.IsTrue(results[0].TeamName == "England");
        }
    }
}
