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
    public class AlbumDataAccessOldTest
    {

        [TestMethod()]
        public void AlbumsExistOldTest()
        {
            //arrange
            AlbumDataAccessOld da = new AlbumDataAccessOld();

            //act
            List<Album> results = da.GetData(true);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public void AlbumsFirstItemOldTest()
        {
            //arrange
            AlbumDataAccessOld da = new AlbumDataAccessOld();
            int albumCode = 14; //The Colour And The Shape

            //act
            Album results = da.GetItem(albumCode, true);

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
        public void AlbumsSaveOldTest()
        {
            //arrange
            AlbumDataAccessOld da = new AlbumDataAccessOld();
            int albumCode = 242;

            //act
            Album results = da.GetItem(albumCode, true);

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
            da.SaveItem(results);

            //reload
            results = da.GetItem(albumCode, true);

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