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
        public async Task ArtistsExistTest()
        {
            //arrange
            ArtistDataAccess da = new ArtistDataAccess();
            bool? includeAllItems = null;

            //act
            List<Artist> results = await da.GetListAsync(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task ArtistsFirstItemTest()
        {
            //arrange
            ArtistDataAccess da = new ArtistDataAccess();
            bool? includeAllItems = null;

            //act
            List<Artist> results = await da.GetListAsync(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ArtistName == "Ash");
            Assert.IsTrue(results[0].ArtistNameTrimed == "Ash");
        }

        [TestMethod()]
        public async Task ArtistsFirstItemIncludeAllTest()
        {
            //arrange
            ArtistDataAccess da = new ArtistDataAccess();
            bool? includeAllItems = true;

            //act
            List<Artist> results = await da.GetListAsync(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ArtistName == "(Top Songs)");
            Assert.IsTrue(results[0].ArtistNameTrimed == "(TopSongs)");
        }

        [TestMethod()]
        public async Task ArtistsIncludeAllItemsExistTest()
        {
            //arrange
            ArtistDataAccess da = new ArtistDataAccess();
            bool? includeAllItems = true;

            //act
            List<Artist> results = await da.GetListAsync(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

    }
}


