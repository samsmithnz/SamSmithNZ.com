﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    public class TournamentTopGoalScorerTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task TournamentTopGoalScorerTest()
        {
            //arrange
            TournamentTopGoalScorerController controller = new(new TournamentTopGoalScorerDataAccess(base.Configuration));
            int tournamentCode = 21;

            //act
            List<TournamentTopGoalScorer> results = await controller.GetTournamentTopGoalScorers(tournamentCode);

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
