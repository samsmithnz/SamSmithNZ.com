using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GamesTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task GamesExistTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";
            bool includeGoals = false;

            //act
            List<Game> results = await controller.GetGames(tournamentCode, roundNumber, roundCode, includeGoals);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public async Task GamesExist2Test()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int tournamentCode = 19;
            int roundNumber = 2;
            //string roundCode = "A";
            bool includeGoals = false;

            //act
            List<Game> results = await controller.GetPlayoffGames(tournamentCode, roundNumber, includeGoals);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task GamesFirstItemTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";
            bool includeGoals = false;

            //act
            List<Game> results = await controller.GetGames(tournamentCode, roundNumber, roundCode, includeGoals);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (Game result in results)
            {
                if (result.GameCode == 11)
                {
                    TestGame(result);
                    break;
                }
            }
        }

        [TestMethod()]
        public async Task GameTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int gameCode = 11;

            //act
            Game result = await controller.GetGame(gameCode);

            //assert
            Assert.IsTrue(result != null);
            TestGame(result);
        }

        private static bool TestGame(Game result)
        {
            Assert.IsTrue(result.CoachFlag == null);
            Assert.IsTrue(result.CoachName == null);
            Assert.IsTrue(result.GameCode > 0);
            Assert.IsTrue(result.GameNumber > 0);
            Assert.IsTrue(result.GameTime > DateTime.MinValue);
            Assert.IsTrue(result.Location != "");
            Assert.IsTrue(result.RoundCode == "F");
            Assert.IsTrue(result.RoundName != "");
            Assert.IsTrue(result.RoundNumber > 0);
            Assert.IsTrue(result.Team1Code > 0);
            Assert.IsTrue(result.Team1FlagName != "");
            Assert.IsTrue(result.Team1Name != "");
            Assert.IsTrue(result.Team1NormalTimeScore >= 0);
            Assert.IsTrue(result.Team1ExtraTimeScore == null);
            Assert.IsTrue(result.Team1PenaltiesScore == null);
            Assert.IsTrue(result.Team1Withdrew == false);
            Assert.IsTrue(result.Team2Code > 0);
            Assert.IsTrue(result.Team2FlagName != "");
            Assert.IsTrue(result.Team2Name != "");
            Assert.IsTrue(result.Team2NormalTimeScore >= 0);
            Assert.IsTrue(result.Team2ExtraTimeScore == null);
            Assert.IsTrue(result.Team2PenaltiesScore == null);
            Assert.IsTrue(result.Team2Withdrew == false);
            Assert.IsTrue(result.TournamentCode == 19);
            Assert.IsTrue(result.TournamentName != "");
            Assert.IsTrue(result.IsOwnGoal == false);
            Assert.IsTrue(result.IsPenalty == false);
            Assert.IsTrue(result.RowType == 1);
            return true;
        }



        [TestMethod]
        public async Task GamesGermanyPlayoffPenaltiesScoreTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int tournamentCode = 18;
            int roundNumber = 1;
            int gameCode = 125; //WC, SF
            int teamCode = 12; //Germany
            bool includeGoals = false;

            //act
            List<Game> results = await controller.GetPlayoffGames(tournamentCode, roundNumber, includeGoals);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (Game item in results)
            {
                if (item.GameCode == gameCode)
                {
                    TestGermany2006WorldCupSF(item, gameCode, teamCode);
                    break;
                }
            }
        }

        [TestMethod]
        public async Task GamesGermanyAETScoreTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int gameCode = 127; //WC, 3P
            int teamCode = 12; //Germany

            //act
            List<Game> results = await controller.GetGamesByTeam(teamCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (Game item in results)
            {
                if (item.GameCode == gameCode)
                {
                    Assert.IsTrue(item.CoachFlag != null);
                    Assert.IsTrue(item.CoachName != null);
                    Assert.IsTrue(item.GameCode == gameCode);
                    Assert.IsTrue(item.GameNumber > 0);
                    Assert.IsTrue(item.GameTime > DateTime.MinValue);
                    Assert.IsTrue(item.Location != "");
                    Assert.IsTrue(item.RoundCode == "3P");
                    Assert.IsTrue(item.RoundName != "");
                    Assert.IsTrue(item.RoundNumber == 2);
                    Assert.IsTrue(item.Team1Code == teamCode);
                    Assert.IsTrue(item.Team1FlagName != "");
                    Assert.IsTrue(item.Team1Name == "Germany");
                    Assert.IsTrue(item.Team1NormalTimeScore == 3);
                    Assert.IsTrue(item.Team1ExtraTimeScore == null);
                    Assert.IsTrue(item.Team1PenaltiesScore == null);
                    Assert.IsTrue(item.Team1Withdrew == false);
                    Assert.IsTrue(item.Team2Code > 0);
                    Assert.IsTrue(item.Team2FlagName != "");
                    Assert.IsTrue(item.Team2Name == "Portugal");
                    Assert.IsTrue(item.Team2NormalTimeScore == 1);
                    Assert.IsTrue(item.Team2ExtraTimeScore == null);
                    Assert.IsTrue(item.Team2PenaltiesScore == null);
                    Assert.IsTrue(item.Team2Withdrew == false);
                    Assert.IsTrue(item.TournamentCode == 18);
                    Assert.IsTrue(item.TournamentName != "");
                    //Results
                    Assert.IsTrue(item.Team1ResultRegulationTimeScore == 3);
                    Assert.IsTrue(item.Team1ResultInformation == "");
                    Assert.IsTrue(item.Team1ResultWonGame == true);
                    Assert.IsTrue(item.Team2ResultRegulationTimeScore == 1);
                    Assert.IsTrue(item.Team2ResultInformation == "");
                    Assert.IsTrue(item.Team2ResultWonGame == false);
                    break;
                }
            }
        }

        [TestMethod]
        public async Task GamesGermanyNormalScoreTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int gameCode = 125; //WC, SF
            int teamCode = 12; //Germany

            //act
            List<Game> results = await controller.GetGamesByTeam(teamCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (Game item in results)
            {
                if (item.GameCode == gameCode)
                {
                    TestGermany2006WorldCupSF(item, gameCode, teamCode);
                    break;
                }
            }
        }

        private static void TestGermany2006WorldCupSF(Game item, int gameCode, int teamCode)
        {
            Assert.IsTrue(item.CoachFlag != null);
            Assert.IsTrue(item.CoachName != null);
            Assert.IsTrue(item.GameCode == gameCode);
            Assert.IsTrue(item.GameNumber > 0);
            Assert.IsTrue(item.GameTime > DateTime.MinValue);
            Assert.IsTrue(item.Location != "");
            Assert.IsTrue(item.RoundCode == "SF");
            Assert.IsTrue(item.RoundName != "");
            Assert.IsTrue(item.RoundNumber == 2);
            Assert.IsTrue(item.Team1Code == teamCode);
            Assert.IsTrue(item.Team1FlagName != "");
            Assert.IsTrue(item.Team1Name == "Germany");
            Assert.IsTrue(item.Team1NormalTimeScore == 0);
            Assert.IsTrue(item.Team1ExtraTimeScore == 0);
            Assert.IsTrue(item.Team1PenaltiesScore == null);
            Assert.IsTrue(item.Team1Withdrew == false);
            Assert.IsTrue(item.Team2Code > 0);
            Assert.IsTrue(item.Team2FlagName != "");
            Assert.IsTrue(item.Team2Name == "Italy");
            Assert.IsTrue(item.Team2NormalTimeScore == 0);
            Assert.IsTrue(item.Team2ExtraTimeScore == 2);
            Assert.IsTrue(item.Team2PenaltiesScore == null);
            Assert.IsTrue(item.Team2Withdrew == false);
            Assert.IsTrue(item.TournamentCode == 18);
            Assert.IsTrue(item.TournamentName != "");
            //Results
            Assert.IsTrue(item.Team1ResultRegulationTimeScore == 0);
            Assert.IsTrue(item.Team1ResultInformation == "");
            Assert.IsTrue(item.Team1ResultWonGame == false);
            Assert.IsTrue(item.Team2ResultRegulationTimeScore == 2);
            Assert.IsTrue(item.Team2ResultInformation == "(aet)");
            Assert.IsTrue(item.Team2ResultWonGame == true);
        }

        [TestMethod]
        public async Task GamesGermanyPenaltiesScoreTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int gameCode = 121; //WC, QF
            int teamCode = 12; //Germany

            //act
            List<Game> results = await controller.GetGamesByTeam(teamCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (Game item in results)
            {
                if (item.GameCode == gameCode)
                {
                    Assert.IsTrue(item.CoachFlag != null);
                    Assert.IsTrue(item.CoachName != null);
                    Assert.IsTrue(item.GameCode == gameCode);
                    Assert.IsTrue(item.GameNumber > 0);
                    Assert.IsTrue(item.GameTime > DateTime.MinValue);
                    Assert.IsTrue(item.Location != "");
                    Assert.IsTrue(item.RoundCode == "QF");
                    Assert.IsTrue(item.RoundName != "");
                    Assert.IsTrue(item.RoundNumber == 2);
                    Assert.IsTrue(item.Team1Code == teamCode);
                    Assert.IsTrue(item.Team1FlagName != "");
                    Assert.IsTrue(item.Team1Name == "Germany");
                    Assert.IsTrue(item.Team1NormalTimeScore == 1);
                    Assert.IsTrue(item.Team1ExtraTimeScore == 0);
                    Assert.IsTrue(item.Team1PenaltiesScore == 4);
                    Assert.IsTrue(item.Team1Withdrew == false);
                    Assert.IsTrue(item.Team2Code > 0);
                    Assert.IsTrue(item.Team2FlagName != "");
                    Assert.IsTrue(item.Team2Name == "Argentina");
                    Assert.IsTrue(item.Team2NormalTimeScore == 1);
                    Assert.IsTrue(item.Team2ExtraTimeScore == 0);
                    Assert.IsTrue(item.Team2PenaltiesScore == 2);
                    Assert.IsTrue(item.Team2Withdrew == false);
                    Assert.IsTrue(item.TournamentCode == 18);
                    Assert.IsTrue(item.TournamentName != "");
                    //Results
                    Assert.IsTrue(item.Team1ResultRegulationTimeScore == 1);
                    Assert.IsTrue(item.Team1ResultInformation == "(pen)");
                    Assert.IsTrue(item.Team1ResultWonGame == true);
                    Assert.IsTrue(item.Team2ResultRegulationTimeScore == 1);
                    Assert.IsTrue(item.Team2ResultInformation == "");
                    Assert.IsTrue(item.Team2ResultWonGame == false);
                    break;
                }
            }
        }

        [TestMethod]
        public async Task GamesArgentinaPenaltiesScoreTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int gameCode = 7389; //WC, SF
            int teamCode = 3; //Argentina

            //act
            List<Game> results = await controller.GetGamesByTeam(teamCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (Game item in results)
            {
                if (item.GameCode == gameCode)
                {
                    Assert.IsTrue(item.CoachFlag != null);
                    Assert.IsTrue(item.CoachName != null);
                    Assert.IsTrue(item.GameCode == gameCode);
                    Assert.IsTrue(item.GameNumber > 0);
                    Assert.IsTrue(item.GameTime > DateTime.MinValue);
                    Assert.IsTrue(item.Location == "");
                    Assert.IsTrue(item.RoundCode == "SF");
                    Assert.IsTrue(item.RoundName != "");
                    Assert.IsTrue(item.RoundNumber == 2);
                    Assert.IsTrue(item.Team1Code > 0);
                    Assert.IsTrue(item.Team1FlagName != "");
                    Assert.IsTrue(item.Team1Name == "Netherlands");
                    Assert.IsTrue(item.Team1NormalTimeScore == 0);
                    Assert.IsTrue(item.Team1ExtraTimeScore == 0);
                    Assert.IsTrue(item.Team1PenaltiesScore == 2);
                    Assert.IsTrue(item.Team1Withdrew == false);
                    Assert.IsTrue(item.Team2Code == teamCode);
                    Assert.IsTrue(item.Team2FlagName != "");
                    Assert.IsTrue(item.Team2Name == "Argentina");
                    Assert.IsTrue(item.Team2NormalTimeScore == 0);
                    Assert.IsTrue(item.Team2ExtraTimeScore == 0);
                    Assert.IsTrue(item.Team2PenaltiesScore == 4);
                    Assert.IsTrue(item.Team2Withdrew == false);
                    Assert.IsTrue(item.TournamentCode == 20);
                    Assert.IsTrue(item.TournamentName != "");
                    //Results
                    Assert.IsTrue(item.Team1ResultRegulationTimeScore == 0);
                    Assert.IsTrue(item.Team1ResultInformation == "");
                    Assert.IsTrue(item.Team1ResultWonGame == false);
                    Assert.IsTrue(item.Team2ResultRegulationTimeScore == 0);
                    Assert.IsTrue(item.Team2ResultInformation == "(pen)");
                    Assert.IsTrue(item.Team2ResultWonGame == true);
                    break;
                }
            }
        }

        [TestMethod]
        public async Task GamesArgentinaAetScoreTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int gameCode = 586; //WC, FF
            int teamCode = 3; //Argentina

            //act
            List<Game> results = await controller.GetGamesByTeam(teamCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            foreach (Game item in results)
            {
                if (item.GameCode == gameCode)
                {
                    Assert.IsTrue(item.CoachFlag != null);
                    Assert.IsTrue(item.CoachName != null);
                    Assert.IsTrue(item.GameCode == gameCode);
                    Assert.IsTrue(item.GameNumber > 0);
                    Assert.IsTrue(item.GameTime > DateTime.MinValue);
                    Assert.IsTrue(item.Location != "");
                    Assert.IsTrue(item.RoundCode == "FF");
                    Assert.IsTrue(item.RoundName != "");
                    Assert.IsTrue(item.RoundNumber == 3);
                    Assert.IsTrue(item.Team1Code > 0);
                    Assert.IsTrue(item.Team1FlagName != "");
                    Assert.IsTrue(item.Team1Name == "Netherlands");
                    Assert.IsTrue(item.Team1NormalTimeScore == 1);
                    Assert.IsTrue(item.Team1ExtraTimeScore == 0);
                    Assert.IsTrue(item.Team1PenaltiesScore == null);
                    Assert.IsTrue(item.Team1Withdrew == false);
                    Assert.IsTrue(item.Team2Code == teamCode);
                    Assert.IsTrue(item.Team2FlagName != "");
                    Assert.IsTrue(item.Team2Name == "Argentina");
                    Assert.IsTrue(item.Team2NormalTimeScore == 1);
                    Assert.IsTrue(item.Team2ExtraTimeScore == 2);
                    Assert.IsTrue(item.Team2PenaltiesScore == null);
                    Assert.IsTrue(item.Team2Withdrew == false);
                    Assert.IsTrue(item.TournamentCode == 11);
                    Assert.IsTrue(item.TournamentName != "");
                    //Results
                    Assert.IsTrue(item.Team1ResultRegulationTimeScore == 1);
                    Assert.IsTrue(item.Team1ResultInformation == "");
                    Assert.IsTrue(item.Team1ResultWonGame == false);
                    Assert.IsTrue(item.Team2ResultRegulationTimeScore == 3);
                    Assert.IsTrue(item.Team2ResultInformation == "(aet)");
                    Assert.IsTrue(item.Team2ResultWonGame == true);
                    break;
                }
            }
        }

        [TestMethod]
        public void GamesTeam2IsWayBetterELOTest()
        {
            //arrange
            Game game = new();
            game.Team1Code = 1;
            game.Team2Code = 2;
            game.Team1PreGameEloRating = 1000;
            game.Team1PostGameEloRating = 1000;
            game.Team2PreGameEloRating = 2000;
            game.Team2PostGameEloRating = 2000;

            //act

            //assert
            Assert.IsTrue(game.Team1ChanceToWin < game.Team2ChanceToWin);
        }

        [TestMethod]
        public void GamesTeamsAreEqualELOTest()
        {
            //arrange
            Game game = new();
            game.Team1Code = 1;
            game.Team2Code = 2;
            game.Team1PreGameEloRating = 2000;
            game.Team1PostGameEloRating = 2000;
            game.Team2PreGameEloRating = 2000;
            game.Team2PostGameEloRating = 2000;

            //act

            //assert
            Assert.IsTrue(game.Team1ChanceToWin == game.Team2ChanceToWin);
        }

        [TestMethod]
        public void GamesTeamsAreEqualELONoTeamsTest()
        {
            //arrange
            Game game = new();
            game.Team1PreGameEloRating = 2000;
            game.Team1PostGameEloRating = 2000;
            game.Team2PreGameEloRating = 2000;
            game.Team2PostGameEloRating = 2000;

            //act

            //assert
            Assert.IsTrue(game.Team1ChanceToWin == game.Team2ChanceToWin && game.Team1ChanceToWin == -1);
        }

        [TestMethod]
        public void Team1TotalGoalsTest()
        {
            //arrange
            Game game = new();
            game.Team1NormalTimeScore = 1;
            game.Team1ExtraTimeScore = 2;

            //act

            //assert
            Assert.IsTrue(game.Team1TotalGoals == 3);
        }

        [TestMethod]
        public void Team2TotalGoalsTest()
        {
            //arrange
            Game game = new();
            game.Team2NormalTimeScore = 1;
            game.Team2ExtraTimeScore = 2;

            //act

            //assert
            Assert.IsTrue(game.Team2TotalGoals == 3);
        }

        [TestMethod]
        public async Task GamesEnglandOddsTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int teamCode = 10; //England
            int gamesExpectedWon = 0;
            int gamesExpectedLoss = 0;
            int gamesUnexpectedWin = 0;
            int gamesUnexpectedLoss = 0;
            int gamesUnexpectedDraw = 0;
            int gamesUnknown = 0;
            int gamesNotPlayed = 0;

            //act
            List<Game> results = await controller.GetGamesByTeam(teamCode);
            foreach (Game item in results)
            {
                if (teamCode == item.Team1Code)
                {
                    gamesExpectedWon += item.Team1OddsCountExpectedWin;
                    gamesExpectedLoss += item.Team1OddsCountExpectedLoss;
                    gamesUnexpectedWin += item.Team1OddsCountUnexpectedWin;
                    gamesUnexpectedLoss += item.Team1OddsCountUnexpectedLoss;
                    gamesUnexpectedDraw += item.Team1OddsCountUnexpectedDraw;
                    gamesUnknown += item.Team1OddsCountUnknown;
                    if (item.Team1NormalTimeScore == null)
                    {
                        gamesNotPlayed++;
                    }
                }
                else if (teamCode == item.Team2Code)
                {
                    gamesExpectedWon += item.Team2OddsCountExpectedWin;
                    gamesExpectedLoss += item.Team2OddsCountExpectedLoss;
                    gamesUnexpectedWin += item.Team2OddsCountUnexpectedWin;
                    gamesUnexpectedLoss += item.Team2OddsCountUnexpectedLoss;
                    gamesUnexpectedDraw += item.Team2OddsCountUnexpectedDraw;
                    gamesUnknown += item.Team2OddsCountUnknown;
                    if (item.Team2NormalTimeScore == null)
                    {
                        gamesNotPlayed++;
                    }
                }
            }

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            int totalGamesCheck = (gamesExpectedWon + gamesExpectedLoss + gamesUnexpectedWin + gamesUnexpectedLoss + gamesUnexpectedDraw + gamesUnknown + gamesNotPlayed);
            Assert.AreEqual(totalGamesCheck, results.Count);
        }

        [TestMethod]
        public async Task GamesNewZealandOddsTest()
        {
            //arrange
            GameController controller = new(new GameDataAccess(base.Configuration));
            int teamCode = 1; //NewZealand
            int gamesExpectedWon = 0;
            int gamesExpectedLoss = 0;
            int gamesUnexpectedWin = 0;
            int gamesUnexpectedLoss = 0;
            int gamesUnexpectedDraw = 0;
            int gamesUnknown = 0;

            //act
            List<Game> results = await controller.GetGamesByTeam(teamCode);
            foreach (Game item in results)
            {
                if (teamCode == item.Team1Code)
                {
                    gamesExpectedWon += item.Team1OddsCountExpectedWin;
                    gamesExpectedLoss += item.Team1OddsCountExpectedLoss;
                    gamesUnexpectedWin += item.Team1OddsCountUnexpectedWin;
                    gamesUnexpectedLoss += item.Team1OddsCountUnexpectedLoss;
                    gamesUnexpectedDraw += item.Team1OddsCountUnexpectedDraw;
                    gamesUnknown += item.Team1OddsCountUnknown;
                }
                else if (teamCode == item.Team2Code)
                {
                    gamesExpectedWon += item.Team2OddsCountExpectedWin;
                    gamesExpectedLoss += item.Team2OddsCountExpectedLoss;
                    gamesUnexpectedWin += item.Team2OddsCountUnexpectedWin;
                    gamesUnexpectedLoss += item.Team2OddsCountUnexpectedLoss;
                    gamesUnexpectedDraw += item.Team2OddsCountUnexpectedDraw;
                    gamesUnknown += item.Team2OddsCountUnknown;
                }
            }

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            int totalGamesCheck = (gamesExpectedWon + gamesExpectedLoss + gamesUnexpectedWin + gamesUnexpectedLoss + gamesUnexpectedDraw + gamesUnknown);
            Assert.IsTrue(totalGamesCheck == results.Count);
            Assert.IsTrue(gamesUnexpectedDraw >= 3);
        }

    }
}
