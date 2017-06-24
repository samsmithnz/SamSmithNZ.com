using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.ITunes.Data;
using SSNZ.ITunes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSNZ.ITunes.UnitTests
{
    [TestClass]
    public class MovementTests
    {
        [TestMethod]
        public async Task MovementTest()
        {
            //Arrange
            MovementDataAccess da = new MovementDataAccess();
            int playlistCode = 1;
            bool showJustSummary = true;

            //Act
            List<Movement> items = await da.GetListAsync(playlistCode, showJustSummary);

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].TrackName == "Frank Black - Song Of The Shrimp");
            Assert.IsTrue(items[0].PlayCount == 36);
            Assert.IsTrue(items[0].ChangeThisMonth == 36);
        }

        [TestMethod]
        public async Task MovementSummaryTest()
        {
            //Arrange
            MovementDataAccess da = new MovementDataAccess();
            bool showJustSummary = true;

            //Act
            List<Movement> items = await da.GetListAsync(showJustSummary);

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].TrackName == "Pete Yorn - For Nancy ('Cos It Already Is)");
            Assert.IsTrue(items[0].PlayCount == 356);
            Assert.IsTrue(items[0].ChangeThisMonth == 356);
        }
    }
}
