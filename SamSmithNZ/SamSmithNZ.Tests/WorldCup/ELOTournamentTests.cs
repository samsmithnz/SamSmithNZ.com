using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ELOTournamentTests : BaseIntegrationTest
    {
        //[TestMethod()]
        //public async Task ELOTournamentProcessingTest()
        //{
        //    EloRatingDataAccess da = new();
        //    int tournamentCode = 21;

        //    //act
        //    List<TeamELORating> results = await da.CalculateEloForTournamentAsync(tournamentCode);

        //    //assert
        //    Assert.IsTrue(results != null);
        //    Assert.IsTrue(results.Count > 0);
        //    foreach (TeamELORating item in results)
        //    {
        //        //System.Diagnostics.Debug.WriteLine(item.TeamName + ": " + item.Rating);
        //        Assert.IsTrue(item.TournamentCode == tournamentCode);
        //        Assert.IsTrue(item.TeamCode > 0);
        //        Assert.IsTrue(item.TeamName != "");
        //        Assert.IsTrue(item.GameCount > 0);
        //        Assert.IsTrue(item.ELORating > 0);
        //        Assert.IsTrue(item.Wins >= 0);
        //        Assert.IsTrue(item.Losses >= 0);
        //        Assert.IsTrue(item.Draws >= 0);
        //        break;
        //    }
        //    //System.Diagnostics.Debug.WriteLine("Done!");
        //}

        //team_a_win_prob = 1.0/(10.0^((team_b - team_a)/400.0) + 1.0)
        [TestMethod()]
        public async Task ELOTournamentRefresh()
        {
            //Arrange
            TournamentController controller = new(new TournamentDataAccess(base.Configuration));
            int competitionCode = 1;
            
            //Act
            List<Tournament> tournaments = await controller.GetTournaments(competitionCode);

            //Assert
            foreach (Tournament tournament in tournaments)
            {
                if (tournament.TournamentCode == 21)
                {
                    EloRatingDataAccess daELO = new(base.Configuration);
                    List<TeamELORating> results = await daELO.UpdateTournamentELORatings(tournament.TournamentCode);

                    Assert.IsTrue(results[0].Draws >= 0);
                    Assert.IsTrue(results[0].ELORating >= 0);
                    Assert.IsTrue(results[0].GameCount >= 0);
                    Assert.IsTrue(results[0].Losses >= 0);
                    Assert.IsTrue(results[0].TeamCode > 0);
                    Assert.IsTrue(results[0].TeamName != "");
                    Assert.IsTrue(results[0].TournamentCode >= 0);
                    Assert.IsTrue(results[0].Wins >= 0);
                }
            }
        }

    }
}
