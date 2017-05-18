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
    public class SongTest
    {

        [TestMethod()]
        public async Task SongsExistTest()
        {
            //arrange
            SongDataAccess da = new SongDataAccess();

            //act
            List<Song> results = await da.GetListAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task SongThisIsACallTest()
        {
            //arrange
            SongDataAccess da = new SongDataAccess();
            int songKey = 1;

            //act
            Song result = await da.GetItemAsync(songKey);

            //assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AlbumKey == 1);
            Assert.IsTrue(result.AlbumName == "Foo Fighters");
            Assert.IsTrue(result.FirstPlayed >= DateTime.MinValue);
            Assert.IsTrue(result.FirstPlayedShowKey == 3);
            Assert.IsTrue(result.LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(result.LastPlayedShowKey > 0);
            Assert.IsTrue(result.SongImage == "images/thisisacall.jpg");
            Assert.IsTrue(result.SongKey == 1);
            Assert.IsTrue(string.IsNullOrEmpty(result.SongLyrics) == false);
            Assert.IsTrue(result.SongName == "This is a Call");
            Assert.IsTrue(string.IsNullOrEmpty(result.SongNotes) == true);
            Assert.IsTrue(result.SongOrder == 1);
            Assert.IsTrue(result.SongTimesPlayed >= 0);
        }

        [TestMethod()]
        public async Task SongsForFooFightersAlbumTest()
        {
            //arrange
            SongDataAccess da = new SongDataAccess();
            int albumKey = 1;

            //act
            List<Song> results = await da.GetListForAlbumAsync(albumKey);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].AlbumKey == 1);
            Assert.IsTrue(results[0].AlbumName == "Foo Fighters");
            Assert.IsTrue(results[0].FirstPlayed >= DateTime.MinValue);
            Assert.IsTrue(results[0].FirstPlayedShowKey == 3);
            Assert.IsTrue(results[0].LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(results[0].LastPlayedShowKey > 0);
            Assert.IsTrue(results[0].SongImage == "images/thisisacall.jpg");
            Assert.IsTrue(results[0].SongKey == 1);
            Assert.IsTrue(string.IsNullOrEmpty(results[0].SongLyrics) == false);
            Assert.IsTrue(results[0].SongName == "This is a Call");
            Assert.IsTrue(string.IsNullOrEmpty(results[0].SongNotes) == true);
            Assert.IsTrue(results[0].SongOrder == 1);
            Assert.IsTrue(results[0].SongTimesPlayed >= 0);
        }

        [TestMethod()]
        public async Task SongShowTest()
        {
            //arrange
            SongDataAccess da = new SongDataAccess();
            int showKey = 3;

            //act
           List< Song> results = await da.GetListForShowAsync(showKey);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[2].AlbumKey == 1);
            Assert.IsTrue(results[2].AlbumName == "Foo Fighters");
            Assert.IsTrue(results[2].FirstPlayed >= DateTime.MinValue);
            Assert.IsTrue(results[2].FirstPlayedShowKey == 3);
            Assert.IsTrue(results[2].LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(results[2].LastPlayedShowKey > 0);
            Assert.IsTrue(results[2].SongImage == "images/thisisacall.jpg");
            Assert.IsTrue(results[2].SongKey == 1);
            Assert.IsTrue(string.IsNullOrEmpty(results[2].SongLyrics) == false);
            Assert.IsTrue(results[2].SongName == "This is a Call");
            Assert.IsTrue(string.IsNullOrEmpty(results[2].SongNotes) == true);
            Assert.IsTrue(results[2].SongOrder == 3);
            Assert.IsTrue(results[2].SongTimesPlayed >= 0);
        }
    }
}