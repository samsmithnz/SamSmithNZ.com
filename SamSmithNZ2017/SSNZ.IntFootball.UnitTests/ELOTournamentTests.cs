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
            int tournamentCode = 21;

            //act
            List<TeamRating> results = await da.CalculateEloForTournamentAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (TeamRating item in results)
            {
                Assert.IsTrue(item.TournamentCode == tournamentCode);
                Assert.IsTrue(item.TeamCode > 0);
                Assert.IsTrue(item.TeamName != "");
                Assert.IsTrue(item.GameCount > 0);
                Assert.IsTrue(item.Rating > 0 );
                Assert.IsTrue(item.Wins >= 0);
                Assert.IsTrue(item.Losses >= 0);
                Assert.IsTrue(item.Draws >= 0);
                break;
            }
        }
    
    }
}
