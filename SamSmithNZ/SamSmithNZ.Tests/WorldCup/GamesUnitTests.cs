using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GamesUnitTests
    {
        [TestMethod]
        public async Task GamesUnitTest()
        {
            //arrange
            Mock<IGameDataAccess> mock = new();
            mock.Setup(repo => repo.GetList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(Task.FromResult(GetGamesTestData()));
            GameController controller = new(mock.Object);
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";
            bool includeGoals = false;

            //act
            List<Game> results = await controller.GetGames(tournamentCode, roundNumber, roundCode, includeGoals);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(14, results[0].TournamentCode);
        }


        [TestMethod]
        public void GameTimeUnitTest()
        {
            //arrange
            Game gameWithTime = new();
            gameWithTime.GameTime = new(2021, 10, 14, 10, 24, 10);
            Game gameNoTime = new();
            gameNoTime.GameTime = new(2021, 10, 14, 0, 0, 0);

            //act
            string gameWithTimeString = gameWithTime.GameTimeString();
            string gameNoTimeString = gameNoTime.GameTimeString();

            //assert
            Assert.AreEqual("14-Oct-2021 10:24", gameWithTimeString);
            Assert.AreEqual("14-Oct-2021", gameNoTimeString);
        }

        private List<Game> GetGamesTestData()
        {
            return new List<Game>() {
            new Game{
                TournamentCode = 14,
                TournamentName = "Test tournament 14"
            } };
        }

    }
}
