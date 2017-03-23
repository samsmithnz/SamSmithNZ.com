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
    public class SearchDataAccessTest
    {

        //1. Search items exist
        [TestMethod()]
        public async Task SearchTabItemsExistTest()
        {
            //arrange
            SearchDataAccess da = new SearchDataAccess();
            Guid recordId = new Guid("7075FE23-B1AC-4169-AC87-D78F1E66BACB");

            //act
            List<Search> results = await da.GetDataAsync(recordId);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 2);
        }

        //3. Specific Search Tab 14
        [TestMethod()]
        public async Task SearchTabFirstItemTest()
        {
            //arrange
            SearchDataAccess da = new SearchDataAccess();
            Guid recordId = new Guid("7075FE23-B1AC-4169-AC87-D78F1E66BACB");

            //act
            List<Search> results = await da.GetDataAsync(recordId);

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
            SearchDataAccess da = new SearchDataAccess();
            string searchText = "home";

            //act part 1
            Guid recordId = await da.SaveItemAsync(searchText);

            //assert part 1
            Assert.IsTrue(recordId != null);
            Assert.IsTrue(recordId != Guid.Empty);

            //act part 2
            List<Search> results = await da.GetDataAsync(recordId);

            //assert part 2
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

    }
}


