using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;
using System.Threading.Tasks;

namespace SSNZ.GuitarTab.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ArtistDataAccessTest
    {
        [TestMethod()]
        public async Task ArtistsExistOldTest()
        {
            //arrange
            ArtistDataAccess da = new ArtistDataAccess();
            int? includeAllItems = null;

            //act
            List<Artist> results = await da.GetDataAsync(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task ArtistsFirstItemOldTest()
        {
            //arrange
            ArtistDataAccess da = new ArtistDataAccess();
            int? includeAllItems = null;

            //act
            List<Artist> results = await da.GetDataAsync(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ArtistName == "(Top Songs)");
            Assert.IsTrue(results[0].ArtistNameTrimed == "(TopSongs)");
        }

        [TestMethod()]
        public async Task ArtistsIncludeAllItemsExistOldTest()
        {
            //arrange
            ArtistDataAccess da = new ArtistDataAccess();
            int? includeAllItems = 1;

            //act
            List<Artist> results = await da.GetDataAsync(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task ArtistsIncludeAllItemsFirstItemOldTest()
        {
            //arrange
            ArtistDataAccess da = new ArtistDataAccess();
            int? includeAllItems = 1;

            //act
            List<Artist> results = await da.GetDataAsync(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ArtistName == "Ash");
            Assert.IsTrue(results[0].ArtistNameTrimed == "Ash");
        }


    }
}


