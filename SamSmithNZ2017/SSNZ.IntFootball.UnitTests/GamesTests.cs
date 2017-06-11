using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SSNZ.IntFootball.UnitTests
{
    [TestClass]
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
            Assert.IsTrue(results[0].Team1ExtraTimeScore == null);
            Assert.IsTrue(results[0].Team1FlagName != "");
            Assert.IsTrue(results[0].Team1Name != "");
            Assert.IsTrue(results[0].Team1NormalTimeScore >= 0);
            Assert.IsTrue(results[0].Team1PenaltiesScore == null);
            Assert.IsTrue(results[0].Team1Withdrew == false);
            Assert.IsTrue(results[0].Team2Code > 0);
            Assert.IsTrue(results[0].Team2ExtraTimeScore == null);
            Assert.IsTrue(results[0].Team2FlagName != "");
            Assert.IsTrue(results[0].Team2Name != "");
            Assert.IsTrue(results[0].Team2NormalTimeScore >= 0);
            Assert.IsTrue(results[0].Team2PenaltiesScore == null);
            Assert.IsTrue(results[0].Team2Withdrew == false);
            Assert.IsTrue(results[0].TournamentCode == 19);
            Assert.IsTrue(results[0].TournamentName != "");
        }

    }
}
;