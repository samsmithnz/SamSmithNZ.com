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
    public class SearchDataAccessTest : BaseIntegrationTest
    {

        //1. Search items exist
        [TestMethod()]
        public async Task SearchTabItemsExistTest()
        {
            //arrange
            SearchController controller = new SearchController(new SearchDataAccess(base.Configuration));
            string searchText = "";
            Guid recordId = new Guid("7075FE23-B1AC-4169-AC87-D78F1E66BACB");

            //act
            List<Search> results = await controller.GetSearchResults(searchText, recordId);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 2);
        }

        //3. Specific Search Tab 14
        [TestMethod()]
        public async Task SearchTabFirstItemTest()
        {
            //arrange
            SearchController controller = new SearchController(new SearchDataAccess(base.Configuration));
            string searchText = "";
            Guid recordId = new Guid("7075FE23-B1AC-4169-AC87-D78F1E66BACB");

            //act
            List<Search> results = await controller.GetSearchResults(searchText, recordId);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            Assert.IsTrue(results[0] != null);
            Assert.IsTrue(results[0].AlbumCode == 14);
            Assert.IsTrue(results[0].ArtistAlbumResult == "Foo Fighters - The Colour And The Shape");
            Assert.IsTrue(results[0].IsBassTab == false);
            Assert.IsTrue(results[0].TrackName == "Everlong");
            Assert.IsTrue(results[0].TrackResult == "11. Everlong");
            Assert.IsTrue(results[0].SearchText == "Everlong");

        }

        [TestMethod()]
        public async Task SearchBigTest()
        {
            //arrange
            SearchController controller = new SearchController(new SearchDataAccess(base.Configuration));
            string searchText = "home";

            //act 
            List<Search> results = await controller.GetSearchResults(searchText);

            //assert 
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 19);
            Assert.IsTrue(results[0] != null);
            Assert.IsTrue(results[0].AlbumCode == 168);
            Assert.IsTrue(results[0].ArtistAlbumResult == "Cure - Disintegration");
            Assert.IsTrue(results[0].IsBassTab == false);
            Assert.IsTrue(results[0].TrackName == "Homesick");
            Assert.IsTrue(results[0].TrackResult == "11. Homesick");
            Assert.IsTrue(results[0].SearchText == "home");
            Assert.IsTrue(results[1] != null);
            Assert.IsTrue(results[1].AlbumCode == 203);
            Assert.IsTrue(results[1].ArtistAlbumResult == "Foo Fighters - Echoes, Silence, Patience And Grace");
            Assert.IsTrue(results[1].IsBassTab == false);
            Assert.IsTrue(results[1].TrackName == "Home");
            Assert.IsTrue(results[1].TrackResult == "12. Home");
            Assert.IsTrue(results[1].SearchText == "home");
        }

        [TestMethod()]
        public async Task SearchEncodedTextTest()
        {
            //arrange
            SearchController controller = new SearchController(new SearchDataAccess(base.Configuration));
            string searchText = "<foo>";

            //act 
            List<Search> results = await controller.GetSearchResults(searchText);

            //assert 
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 0);
        }

    }
}


