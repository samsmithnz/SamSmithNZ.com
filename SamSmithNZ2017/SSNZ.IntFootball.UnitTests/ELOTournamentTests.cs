using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SSNZ.IntFootball.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ELOTournamentTests
    {
        [TestMethod()]
        public async Task ELOTournamentProcessingTest()
        {
            EloRatingDataAccess da = new EloRatingDataAccess();
            int tournamentCode = 19;

            //act
            List<TeamRating> results = await da.CalculateEloForTournamentAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (TeamRating item in results)
            {
                Assert.IsTrue(item.TournamentCode == 19);
                Assert.IsTrue(item.TeamCode == 29);
                Assert.IsTrue(item.TeamName == "Spain");
                Assert.IsTrue(item.GameCount == 7);
                Assert.IsTrue(item.Rating == 1177);
                Assert.IsTrue(item.Wins == 6);
                Assert.IsTrue(item.Losses == 1);
                Assert.IsTrue(item.Draws == 0);
                break;
            }
        }
    
    }
}
