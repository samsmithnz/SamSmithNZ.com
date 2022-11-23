using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.GuitarTab;
using SamSmithNZ.Service.Models.Steam;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SamSmithNZ.Service.DataAccess.WorldCup.EloRating;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ELORatingTests : BaseIntegrationTest
    {

        [TestMethod()]
        public void EvenTeamsELORatingsTest()
        {
            //arrange
            EloRating eloRating = new();
            int team1ELORating = 1000;
            int team2ELORating = 1000; 
            Service.Models.WorldCup.Game game = new()
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 0
            };

            //act
            WhoWonEnum? whoWonGame = eloRating.WhoWon(game);
            double kFactor = eloRating.CalculateKFactor(game);
            (int, int) result = eloRating.GetEloRatingScoresForMatchUp(team1ELORating, team2ELORating,
                whoWonGame == WhoWonEnum.Team1,
                whoWonGame == WhoWonEnum.Team2,
                kFactor);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1050, result.Item1);
            Assert.AreEqual(950, result.Item2);
        }

        [TestMethod()]
        public void GermanyJapan2022ELORatingsTest()
        {
            //arrange
            EloRating eloRating = new();
            int team1ELORating = 1963;
            int team2ELORating = 1798;
            Service.Models.WorldCup.Game game = new()
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 2
            };

            //act
            WhoWonEnum? whoWonGame = eloRating.WhoWon(game);
            double kFactor = eloRating.CalculateKFactor(game);
            (int, int) result = eloRating.GetEloRatingScoresForMatchUp(team1ELORating, team2ELORating,
                whoWonGame == WhoWonEnum.Team1, 
                whoWonGame == WhoWonEnum.Team2,
                kFactor);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1891, result.Item1);
            Assert.AreEqual(1870, result.Item2);
        }


    }
}
