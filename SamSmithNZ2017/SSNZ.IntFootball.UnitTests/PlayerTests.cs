using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;

namespace SSNZ.IntFootball.UnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public async Task PlayersExistTest()
        {
            //arrange
            PlayerDataAccess da = new PlayerDataAccess();
            int gameCode = 7328;

            //act
            List<Player> results = await da.GetListAsync(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task PlayersFirstItemTest()
        {
            //arrange
            PlayerDataAccess da = new PlayerDataAccess();
            int gameCode = 7328;

            //act
            List<Player> results = await da.GetListAsync(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].Number ==2);
            Assert.IsTrue(results[0].PlayerCode > 0);
            Assert.IsTrue(results[0].PlayerName == "Alves, Dani (Brazil)");
            Assert.IsTrue(results[0].Position == "DF");
            Assert.IsTrue(results[0].TeamName == "Brazil");

        }


    }
}
