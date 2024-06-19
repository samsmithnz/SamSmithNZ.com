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


        [TestMethod()]
        public void SemiFinal2022ELORatingsAndOddsTest()
        {
            //arrange
            EloRating eloRating = new();
            int argentina = 2072; // 63.47%
            int croatia = 1976; // 36.53%
            int france = 2120; // 59.52%
            int morrocco = 2053; // 40.48%
            Service.Models.WorldCup.Game game = new()
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 0
            };

            //act
            Service.Models.WorldCup.Game game1 = new();
            game1.Team1PreGameEloRating = argentina;
            game1.Team2PreGameEloRating = croatia;
            game1.GameCanEndInADraw = false;
            double argentinaChanceToWin1 = game1.Team1ChanceToWin;
            Assert.AreEqual(63.47, argentinaChanceToWin1);

            Service.Models.WorldCup.Game game2 = new();
            game2.Team1PreGameEloRating = argentina;
            game2.Team2PreGameEloRating = france;
            game2.GameCanEndInADraw = false;
            double argentinaChanceToWin2 = game2.Team1ChanceToWin;
            Assert.AreEqual(43.14, argentinaChanceToWin2);

            Service.Models.WorldCup.Game game3 = new();
            game3.Team1PreGameEloRating = argentina;
            game3.Team2PreGameEloRating = morrocco;
            game3.GameCanEndInADraw = false;
            double argentinaChanceToWin3 = game3.Team1ChanceToWin;
            Assert.AreEqual(52.73, argentinaChanceToWin3);

            Service.Models.WorldCup.Game game4 = new();
            game4.Team1PreGameEloRating = france;
            game4.Team2PreGameEloRating = croatia;
            game4.GameCanEndInADraw = false;
            double franceChanceToWin4 = game4.Team1ChanceToWin;
            Assert.AreEqual(69.61, franceChanceToWin4);

            Service.Models.WorldCup.Game game5 = new();
            game5.Team1PreGameEloRating = france;
            game5.Team2PreGameEloRating = morrocco;
            game5.GameCanEndInADraw = false;
            double franceChanceToWin5 = game5.Team1ChanceToWin;
            Assert.AreEqual(59.52, franceChanceToWin5);

            Service.Models.WorldCup.Game game6 = new();
            game6.Team1PreGameEloRating = croatia;
            game6.Team2PreGameEloRating = morrocco;
            game6.GameCanEndInADraw = false;
            double croatiaChanceToWin6 = game6.Team1ChanceToWin;
            Assert.AreEqual(39.1, croatiaChanceToWin6);


            //SF1 (2 paths)
            //SF2 (2 paths)
            //Final (2 paths)

            //(int, int) result = eloRating.GetEloRatingScoresForMatchUp(argentina, croatia,
            //WhoWonEnum? whoWonGame = eloRating.WhoWon(game);
            //double kFactor = eloRating.CalculateKFactor(game);
            //(int, int) result = eloRating.GetEloRatingScoresForMatchUp(team1ELORating, team2ELORating,
            //    whoWonGame == WhoWonEnum.Team1,
            //    whoWonGame == WhoWonEnum.Team2,
            //    kFactor);

            ////assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(1891, result.Item1);
            //Assert.AreEqual(1870, result.Item2);
        }

    }
}
