using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.ITunes.Data;
using SSNZ.ITunes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSNZ.ITunes.UnitTests
{
    [TestClass]
    public class PlaylistTests
    {
        [TestMethod]
        public async Task PlayListTest()
        {
            //Arrange
            PlaylistDataAccess da = new PlaylistDataAccess();

            //Act
            List<Playlist> items = await da.GetListAsync();

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[items.Count - 1].PlaylistCode == 1);
            Assert.IsTrue(items[items.Count - 1].PlaylistDate.Year == 2006);
            Assert.IsTrue(items[items.Count - 1].PlaylistDate.Month == 8);
        }
    }
}
