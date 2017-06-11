using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SSNZ.IntFootball.UnitTests
{
    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public async Task GroupsExistTest()
        {
            //arrange
            GroupDataAccess da = new GroupDataAccess();
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";

            //act
            List<Group> results = await da.GetListAsync(tournamentCode, roundNumber, roundCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task GroupsFirstItemTest()
        {
            //arrange
            GroupDataAccess da = new GroupDataAccess();
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";

            //act
            List<Group> results = await da.GetListAsync(tournamentCode, roundNumber, roundCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].Draws >= 0);
            Assert.IsTrue(results[0].GoalDifference >= 0);
            Assert.IsTrue(results[0].GoalsAgainst >= 0);
            Assert.IsTrue(results[0].GoalsFor >= 0);
            Assert.IsTrue(results[0].GroupRanking >= 0);
            Assert.IsTrue(results[0].HasQualifiedForNextRound == true);
            Assert.IsTrue(results[0].Losses >= 0);
            Assert.IsTrue(results[0].Played >= 0);
            Assert.IsTrue(results[0].Points >= 0);
            Assert.IsTrue(results[0].RoundCode == "F");
            Assert.IsTrue(results[0].RoundNumber >= 0);
            Assert.IsTrue(results[0].TeamCode >= 0);
            Assert.IsTrue(results[0].TeamFlagName != "");
            Assert.IsTrue(results[0].TeamName != "");
            Assert.IsTrue(results[0].TournamentCode >= 0);
            Assert.IsTrue(results[0].Wins >= 0);
        }

    }
}
