using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;

namespace SSNZ.GuitarTab.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ArtistDataAccessOldTest
    {
        [TestMethod()]
        public void ArtistsExistOldTest()
        {
            //arrange
            ArtistDataAccessOld da = new ArtistDataAccessOld();
            int? includeAllItems = null;

            //act
            List<Artist> results = da.GetData(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public void ArtistsFirstItemOldTest()
        {
            //arrange
            ArtistDataAccessOld da = new ArtistDataAccessOld();
            int? includeAllItems = null;

            //act
            List<Artist> results = da.GetData(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ArtistName == "(Top Songs)");
            Assert.IsTrue(results[0].ArtistNameTrimed == "(TopSongs)");
        }

        [TestMethod()]
        public void ArtistsIncludeAllItemsExistOldTest()
        {
            //arrange
            ArtistDataAccessOld da = new ArtistDataAccessOld();
            int? includeAllItems = 1;

            //act
            List<Artist> results = da.GetData(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public void ArtistsIncludeAllItemsFirstItemOldTest()
        {
            //arrange
            ArtistDataAccessOld da = new ArtistDataAccessOld();
            int? includeAllItems = 1;

            //act
            List<Artist> results = da.GetData(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].ArtistName == "Ash");
            Assert.IsTrue(results[0].ArtistNameTrimed == "Ash");
        }


    }
}


