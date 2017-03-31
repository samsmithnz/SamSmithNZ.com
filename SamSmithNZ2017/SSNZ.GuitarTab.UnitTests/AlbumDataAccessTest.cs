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
    public class AlbumDataAccessTest
    {

        [TestMethod()]
        public async Task AlbumsExistTest()
        {
            //arrange
            AlbumDataAccess da = new AlbumDataAccess();

            //act
            List<Album> results = await da.GetListAsync(true);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task AlbumsFirstItemTest()
        {
            //arrange
            AlbumDataAccess da = new AlbumDataAccess();
            int albumCode = 14; //The Colour And The Shape

            //act
            Album results = await da.GetItemAsync(albumCode, true);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.AlbumCode == 14);
            Assert.IsTrue(results.AlbumName == "The Colour And The Shape");
            Assert.IsTrue(results.AlbumYear == 1997);
            Assert.IsTrue(results.ArtistName == "Foo Fighters");
            Assert.IsTrue(results.ArtistNameTrimed == "FooFighters");
            Assert.IsTrue(results.AverageRating == 5);
            Assert.IsTrue(results.IncludeInIndex == true);
            Assert.IsTrue(results.IncludeOnWebsite == true);
            Assert.IsTrue(results.IsBassTab == false);
            Assert.IsTrue(results.IsMiscCollectionAlbum == false);
            Assert.IsTrue(results.IsNewAlbum == false);
        }

        [TestMethod()]
        public async Task AlbumsSaveTest()
        {
            //arrange
            AlbumDataAccess da = new AlbumDataAccess();
            int albumCode = 242;

            //act
            Album results = await da.GetItemAsync(albumCode, true);

            //update
            string albumName = "";
            string artistName = "";
            if (results.AlbumName == "Test Album1")
            {
                albumName = "Test Album2";
                artistName = "Test Artist2";
            }
            else
            {
                albumName = "Test Album1";
                artistName = "Test Artist1";
            }
            results.AlbumName = albumName;
            results.ArtistName = artistName;
        await    da.SaveItemAsync(results);

            //reload
            results = await da.GetItemAsync(albumCode, true);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.AlbumCode == 242);
            Assert.IsTrue(results.AlbumName == albumName);
            Assert.IsTrue(results.AlbumYear == 2014);
            Assert.IsTrue(results.ArtistName == artistName);
            Assert.IsTrue(results.ArtistNameTrimed == artistName.Replace(" ", ""));
            Assert.IsTrue(results.AverageRating == 0);
            Assert.IsTrue(results.IncludeInIndex == false);
            Assert.IsTrue(results.IncludeOnWebsite == false);
            Assert.IsTrue(results.IsBassTab == false);
            Assert.IsTrue(results.IsMiscCollectionAlbum == false);
            Assert.IsTrue(results.IsNewAlbum == false);
        }
    }
}