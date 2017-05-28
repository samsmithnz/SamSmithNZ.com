using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.ITunes.Data;
using SSNZ.ITunes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSNZ.ITunes.UnitTests
{
    [TestClass]
    public class TopArtistsTests
    {
        [TestMethod]
        public async Task TopArtistsTest()
        {
            //Arrange
            TopArtistsDataAccess da = new TopArtistsDataAccess();
            int playlistCode = 1;
            bool showJustSummary = true;

            //Act
            List<TopArtists> items = await da.GetListAsync(playlistCode, showJustSummary);

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].ArtistCount == 7);
            Assert.IsTrue(items[0].ArtistName == "Betchadupa");
        }
    }
}
