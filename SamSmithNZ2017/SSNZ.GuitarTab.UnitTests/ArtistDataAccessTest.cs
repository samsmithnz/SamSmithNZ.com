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
    public class ArtistDataAccessTest
    {
        [TestMethod()]
        public async Task ArtistsExistTest()
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
        public async Task ArtistsIncludeAllItemsExistTest()
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


    }
}


