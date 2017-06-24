using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.FooFighters.Data;
using SSNZ.FooFighters.Models;
using System.Threading.Tasks;

namespace SSNZ.GuitarTab.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AverageSongTest
    {

        [TestMethod()]
        public async Task AverageSetlistExistTest()
        {
            //arrange
            AverageSetlistDataAccess da = new AverageSetlistDataAccess();
            int yearCode = 2015;
            int minimumSongCount = 0;
            bool showAllSongs = true;

            //act
            List<AverageSetlist> results = await da.GetListAsync(yearCode, minimumSongCount, showAllSongs);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task AverageSetlist2015Test()
        {
            //arrange
            AverageSetlistDataAccess da = new AverageSetlistDataAccess();
            int yearCode = 2015;
            int minimumSongCount = 0;
            bool showAllSongs = true;

            //act
            List<AverageSetlist> results = await da.GetListAsync(yearCode, minimumSongCount, showAllSongs);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].SongCode > 0);
            Assert.IsTrue(results[0].SongName != "");
            Assert.IsTrue(results[0].AvgShowSongOrder>0);
            Assert.IsTrue(results[0].SongCount > 0);
            Assert.IsTrue(results[0].SongRank > 0);
            Assert.IsTrue(results[0].YearCode > 0);
        }
       
    }
}