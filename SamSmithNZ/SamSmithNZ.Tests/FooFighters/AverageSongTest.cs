using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;
using System.Threading.Tasks;
using SamSmithNZ.Service.Controllers.FooFighters;
using SamSmithnNZ.Tests;

namespace SamSmithNZ.Tests.FooFighters
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AverageSongTest : BaseIntegrationTest
    {

        [TestMethod()]
        public async Task AverageSetlistExistTest()
        {
            //arrange
            AverageSetlistController controller = new AverageSetlistController(new AverageSetlistDataAccess(base.Configuration));
            int yearCode = 2015;
            int minimumSongCount = 0;
            bool showAllSongs = true;

            //act
            List<AverageSetlist> items = await controller.GetAverageSetlist(yearCode, minimumSongCount, showAllSongs);

            //assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod()]
        public async Task AverageSetlist2015Test()
        {
            //arrange
            AverageSetlistController controller = new AverageSetlistController(new AverageSetlistDataAccess(base.Configuration));
            int yearCode = 2015;
            int minimumSongCount = 0;
            bool showAllSongs = true;

            //act
            List<AverageSetlist> items = await controller.GetAverageSetlist(yearCode, minimumSongCount, showAllSongs);

            //assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].SongCode > 0);
            Assert.IsTrue(items[0].SongName != "");
            Assert.IsTrue(items[0].AvgShowSongOrder > 0);
            Assert.IsTrue(items[0].SongCount > 0);
            Assert.IsTrue(items[0].SongRank > 0);
            Assert.IsTrue(items[0].YearCode > 0);
        }

    }
}