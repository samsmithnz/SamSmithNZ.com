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
        //public async Task RefreshELORatingsTest()
        //{
        //    //arrange
        //    ELORatingController controller = new(
        //        new EloRatingDataAccess(base.Configuration), 
        //        new GameDataAccess(base.Configuration));
        //    int tournamentCode = 22;

        //    //act
        //    bool result = await controller.RefreshTournamentELORatings(tournamentCode);

        //    //assert
        //    Assert.IsTrue(result);
        //}

        //team_a_win_prob = 1.0/(10.0^((team_b - team_a)/400.0) + 1.0)
        [TestMethod()]
        public async Task ELOTournamentRefresh()
        {
            //Arrange
            TournamentController controller = new(new TournamentDataAccess(base.Configuration));
            int tournamentCode = 21;

            //Act
            Tournament tournament = await controller.GetTournament(tournamentCode);

            //Assert
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

            Assert.AreEqual(2239, results[0].ELORating);
            Assert.AreEqual(1964, results[5].ELORating);
            Assert.AreEqual(1519, results[^1].ELORating);
        }

    }
}
