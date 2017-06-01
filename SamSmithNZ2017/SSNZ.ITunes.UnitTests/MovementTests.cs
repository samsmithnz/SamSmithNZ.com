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
    }
}
