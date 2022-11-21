using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TeamStatisticsTests : BaseIntegrationTest
    {

        [TestMethod()]
        public async Task GetMatchUpForGameTest()
        {
            //arrange
            TeamStatisticsController controller = new(
                new TeamDataAccess(base.Configuration),
                new GameDataAccess(base.Configuration));
            int team1Code = 84;
            int team2Code = 38;

            //act
            TeamMatchup result = await controller.GetTeamMatchup(team1Code, team2Code);

            //assert
            Assert.IsNotNull(result);
        }



    }
}
