using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.ITunes.Data;
using SSNZ.ITunes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSNZ.ITunes.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlaylistTests
    {
        [TestMethod]
        public async Task PlayListTest()
        {
            //Arrange
            PlaylistDataAccess da = new PlaylistDataAccess();

            //Act
            List<Playlist> items = await da.GetListAsync();

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[items.Count - 1].PlaylistCode == 1);
            Assert.IsTrue(items[items.Count - 1].PlaylistDate.Year == 2006);
            Assert.IsTrue(items[items.Count - 1].PlaylistDate.Month == 8);
        }

        [TestMethod]
        public async Task PlayListItemTest()
        {
            //Arrange
            PlaylistDataAccess da = new PlaylistDataAccess();
            int playlistCode = 1;
            //Act
            Playlist item = await da.GetItemAsync(playlistCode);

            //Assert
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.PlaylistCode == 1);
            Assert.IsTrue(item.PlaylistDate.Year == 2006);
            Assert.IsTrue(item.PlaylistDate.Month == 8);
        }

        [TestMethod]
        public async Task BigComplicatedPlaylistTest()
        {
            PlaylistDataAccess da = new PlaylistDataAccess();
            DateTime newDate = new DateTime(2020, 1, 1);

            //Act
            //delete the playlist if it exists - this is essentially a special cleanup step if the last step failed.
            List<Playlist> items = await da.GetListAsync();
            Playlist item = GetTestPlaylist(newDate, items);
            if (item != null)
            {
                await da.DeleteItemAsync(item.PlaylistCode);
            }

            //add the new playlist
            await da.SaveItemAsync(newDate);
            items = await da.GetListAsync();
            item = GetTestPlaylist(newDate, items);

            //Run the special track functions
            TrackDataAccess da2 = new TrackDataAccess();

            Track track = new Track();
            track.PlaylistCode = item.PlaylistCode;
            track.AlbumName = "Test album";
            track.ArtistName = "Test artist";
            track.RecordId = Guid.NewGuid();
            track.TrackName = "Test track name";
            await da2.SaveItemAsync(track);
            await da2.SetTrackRanksForPlaylistAsync(item.PlaylistCode);
            await da2.ValidateTracksForPlaylistAsync(item.PlaylistCode);

            //Clean up and delete the playlist
            items = await da.GetListAsync();
            item = GetTestPlaylist(newDate, items);
            if (item != null)
            {
                await da.DeleteItemAsync(item.PlaylistCode);
            }

            //No Asserts. Just run the code and don't crash...
        }

        private Playlist GetTestPlaylist(DateTime newDate, List<Playlist> items)
        {
            foreach (Playlist item in items)
            {
                if (item.PlaylistDate == newDate)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
