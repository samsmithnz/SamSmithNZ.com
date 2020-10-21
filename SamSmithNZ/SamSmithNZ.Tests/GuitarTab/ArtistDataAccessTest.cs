using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ArtistDataAccessTest : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task ArtistsExistTest()
        {
            //arrange
            ArtistController controller = new ArtistController(new ArtistDataAccess(base.Configuration));
            bool includeAllItems = false;

            //act
            List<Artist> results = await controller.GetArtists(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task ArtistsFirstItemTest()
        {
            //arrange
            ArtistController controller = new ArtistController(new ArtistDataAccess(base.Configuration));
            bool includeAllItems = false;

            //act
            List<Artist> results = await controller.GetArtists(includeAllItems);

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
            ArtistController controller = new ArtistController(new ArtistDataAccess(base.Configuration));
            bool includeAllItems = true;

            //act
            List<Artist> results = await controller.GetArtists(includeAllItems);

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
            ArtistController controller = new ArtistController(new ArtistDataAccess(base.Configuration));
            bool includeAllItems = true;

            //act
            List<Artist> results = await controller.GetArtists(includeAllItems);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

    }
}


