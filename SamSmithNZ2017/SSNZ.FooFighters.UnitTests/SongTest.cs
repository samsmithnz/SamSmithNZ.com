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
            Assert.IsTrue(result.AlbumCode == 1);
            Assert.IsTrue(result.AlbumName == "Foo Fighters");
            Assert.IsTrue(result.FirstPlayed >= DateTime.MinValue);
            Assert.IsTrue(result.FirstPlayedShowCode == 3);
            Assert.IsTrue(result.LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(result.LastPlayedShowCode > 0);
            Assert.IsTrue(result.SongImage == "images/thisisacall.jpg");
            Assert.IsTrue(result.SongCode == 1);
            Assert.IsTrue(string.IsNullOrEmpty(result.SongLyrics) == false);
            Assert.IsTrue(result.SongName == "This is a Call");
            Assert.IsTrue(string.IsNullOrEmpty(result.SongNotes) == true);
            Assert.IsTrue(result.SongOrder == 1);
            Assert.IsTrue(result.TimesPlayed > 0);
        }

        [TestMethod()]
        public async Task SomeThingFromNothingNullDatesTest()
        {
            //arrange
            SongDataAccess da = new SongDataAccess();
            int songKey = 318;

            //act
            Song result = await da.GetItemAsync(songKey);

            //assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AlbumCode == 14);
            Assert.IsTrue(result.AlbumName == "Sonic Highways");
            Assert.IsTrue(result.FirstPlayed != null);
            Assert.IsTrue(result.FirstPlayedShowCode != null);
            Assert.IsTrue(result.LastPlayed != null);
            Assert.IsTrue(result.LastPlayedShowCode != null);
            Assert.IsTrue(result.SongImage == null);
            Assert.IsTrue(result.SongCode == 318);
            Assert.IsTrue(string.IsNullOrEmpty(result.SongLyrics) == true);
            Assert.IsTrue(result.SongName == "Something From Nothing");
            Assert.IsTrue(string.IsNullOrEmpty(result.SongNotes) == true);
            Assert.IsTrue(result.SongOrder == 1);
            Assert.IsTrue(result.TimesPlayed > 0);
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
            Assert.IsTrue(results[0].AlbumCode == 1);
            Assert.IsTrue(results[0].AlbumName == "Foo Fighters");
            Assert.IsTrue(results[0].FirstPlayed >= DateTime.MinValue);
            Assert.IsTrue(results[0].FirstPlayedShowCode == 3);
            Assert.IsTrue(results[0].LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(results[0].LastPlayedShowCode > 0);
            Assert.IsTrue(results[0].SongImage == "images/thisisacall.jpg");
            Assert.IsTrue(results[0].SongCode == 1);
            Assert.IsTrue(string.IsNullOrEmpty(results[0].SongLyrics) == false);
            Assert.IsTrue(results[0].SongName == "This is a Call");
            Assert.IsTrue(string.IsNullOrEmpty(results[0].SongNotes) == true);
            Assert.IsTrue(results[0].SongOrder == 1);
            Assert.IsTrue(results[0].TimesPlayed > 0);
        }

        [TestMethod()]
        public async Task SongShowTest()
        {
            //arrange
            SongDataAccess da = new SongDataAccess();
            int showKey = 3;

            //act
            List<Song> results = await da.GetListForShowAsync(showKey);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[2].AlbumCode == 1);
            Assert.IsTrue(results[2].AlbumName == "Foo Fighters");
            Assert.IsTrue(results[2].FirstPlayed >= DateTime.MinValue);
            Assert.IsTrue(results[2].FirstPlayedShowCode == 3);
            Assert.IsTrue(results[2].LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(results[2].LastPlayedShowCode > 0);
            Assert.IsTrue(results[2].SongImage == "images/thisisacall.jpg");
            Assert.IsTrue(results[2].SongCode == 1);
            Assert.IsTrue(string.IsNullOrEmpty(results[2].SongLyrics) == false);
            Assert.IsTrue(results[2].SongName == "This is a Call");
            Assert.IsTrue(string.IsNullOrEmpty(results[2].SongNotes) == true);
            Assert.IsTrue(results[2].SongOrder == 3);
            Assert.IsTrue(results[2].TimesPlayed > 0);
        }
    }
}