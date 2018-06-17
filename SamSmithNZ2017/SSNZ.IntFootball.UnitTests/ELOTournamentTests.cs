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
            List<TeamELORating> results = await da.CalculateEloForTournamentAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (TeamELORating item in results)
            {
                //System.Diagnostics.Debug.WriteLine(item.TeamName + ": " + item.Rating);
                Assert.IsTrue(item.TournamentCode == tournamentCode);
                Assert.IsTrue(item.TeamCode > 0);
                Assert.IsTrue(item.TeamName != "");
                Assert.IsTrue(item.GameCount > 0);
                Assert.IsTrue(item.ELORating > 0);
                Assert.IsTrue(item.Wins >= 0);
                Assert.IsTrue(item.Losses >= 0);
                Assert.IsTrue(item.Draws >= 0);
                break;
            }
            //System.Diagnostics.Debug.WriteLine("Done!");
        }


        [TestMethod()]
        public async Task ELOTournamentRefresh()
        {
            TournamentDataAccess daTournament = new TournamentDataAccess();
            List<Tournament> tournaments = await daTournament.GetListAsync();

            foreach (Tournament tournament in tournaments)
            {
                if (tournament.TournamentCode ==21)
                {
                    EloRatingDataAccess daELO = new EloRatingDataAccess();
                    List<TeamELORating> teams = await daELO.CalculateEloForTournamentAsync(tournament.TournamentCode);

                    foreach (TeamELORating team in teams)
                    {
                        bool result = await daELO.SaveTeamELORatingAsync(team.TournamentCode, team.TeamCode, team.ELORating);
                        Assert.IsTrue(result);

                    }
                }
            }
        }

    }
}
