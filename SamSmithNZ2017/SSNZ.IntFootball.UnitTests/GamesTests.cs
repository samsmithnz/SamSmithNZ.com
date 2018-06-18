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
    public class GamesTests
    {
        [TestMethod]
        public async Task GamesExistTest()
        {
            //arrange
            GameDataAccess da = new GameDataAccess();
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";

            //act
            List<Game> results = await da.GetListAsync(tournamentCode, roundNumber, roundCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public async Task GamesExist2Test()
        {
            //arrange
            GameDataAccess da = new GameDataAccess();
            int tournamentCode = 19;
            int roundNumber = 2;
            //string roundCode = "A";

            //act
            List<Game> results = await da.GetListAsyncByPlayoff(tournamentCode, roundNumber);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task GamesFirstItemTest()
        {
            //arrange
            GameDataAccess da = new GameDataAccess();
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";

            //act
            List<Game> results = await da.GetListAsync(tournamentCode, roundNumber, roundCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].CoachFlag == null);
            Assert.IsTrue(results[0].CoachName == null);
            Assert.IsTrue(results[0].GameCode > 0);
            Assert.IsTrue(results[0].GameNumber > 0);
            Assert.IsTrue(results[0].GameTime > DateTime.MinValue);
            Assert.IsTrue(results[0].Location != "");
            Assert.IsTrue(results[0].RoundCode == "F");
            Assert.IsTrue(results[0].RoundName != "");
            Assert.IsTrue(results[0].RoundNumber > 0);
            Assert.IsTrue(results[0].Team1Code > 0);
            Assert.IsTrue(results[0].Team1FlagName != "");
            Assert.IsTrue(results[0].Team1Name != "");
            Assert.IsTrue(results[0].Team1NormalTimeScore >= 0);
            Assert.IsTrue(results[0].Team1ExtraTimeScore == null);
            Assert.IsTrue(results[0].Team1PenaltiesScore == null);
            Assert.IsTrue(results[0].Team1Withdrew == false);
            Assert.IsTrue(results[0].Team2Code > 0);
            Assert.IsTrue(results[0].Team2FlagName != "");
            Assert.IsTrue(results[0].Team2Name != "");
            Assert.IsTrue(results[0].Team2NormalTimeScore >= 0);
            Assert.IsTrue(results[0].Team2ExtraTimeScore == null);
            Assert.IsTrue(results[0].Team2PenaltiesScore == null);
            Assert.IsTrue(results[0].Team2Withdrew == false);
            Assert.IsTrue(results[0].TournamentCode == 19);
            Assert.IsTrue(results[0].TournamentName != "");
            Assert.IsTrue(results[0].IsOwnGoal == false);
            Assert.IsTrue(results[0].IsPenalty == false);
            Assert.IsTrue(results[0].RowType == 1);
        }

        [TestMethod]
        public async Task GamesGermanyPlayoffPenaltiesScoreTest()
        {
            //arrange
            GameDataAccess da = new GameDataAccess();
            int tournamentCode = 18;
            int roundNumber = 1;
            int gameCode = 125; //WC, SF
            int teamCode = 12; //Germany

            //act
            List<Game> results = await da.GetListAsyncByPlayoff(tournamentCode, roundNumber);

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
            GameDataAccess da = new GameDataAccess();
            int gameCode = 127; //WC, 3P
            int teamCode = 12; //Germany

            //act
            List<Game> results = await da.GetListAsyncByTeam(teamCode);

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
            GameDataAccess da = new GameDataAccess();
            int gameCode = 125; //WC, SF
            int teamCode = 12; //Germany

            //act
            List<Game> results = await da.GetListAsyncByTeam(teamCode);

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

        private void TestGermany2006WorldCupSF(Game item, int gameCode, int teamCode)
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
            GameDataAccess da = new GameDataAccess();
            int gameCode = 121; //WC, QF
            int teamCode = 12; //Germany

            //act
            List<Game> results = await da.GetListAsyncByTeam(teamCode);

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
            GameDataAccess da = new GameDataAccess();
            int gameCode = 7389; //WC, SF
            int teamCode = 3; //Argentina

            //act
            List<Game> results = await da.GetListAsyncByTeam(teamCode);

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
            GameDataAccess da = new GameDataAccess();
            int gameCode = 586; //WC, FF
            int teamCode = 3; //Argentina

            //act
            List<Game> results = await da.GetListAsyncByTeam(teamCode);

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
            Game game = new Game();
            game.Team1EloRating = 1000;
            game.Team2EloRating = 2000;

            //act

            //assert
            Assert.IsTrue(game.Team1ChanceToWin < game.Team2ChanceToWin);
        }

        [TestMethod]
        public void GamesTeamsAreEqualELOTest()
        {
            //arrange
            Game game = new Game();
            game.Team1EloRating = 1000;
            game.Team2EloRating = 1000;

            //act

            //assert
            Assert.IsTrue(game.Team1ChanceToWin == game.Team2ChanceToWin);
        }
    }
}
