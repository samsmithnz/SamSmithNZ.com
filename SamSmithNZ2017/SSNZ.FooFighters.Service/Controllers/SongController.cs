using SSNZ.FooFighters.Data;
using SSNZ.FooFighters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.FooFighters.Service.Controllers
{
    public class SongController : ApiController
    {
        public async Task<List<Song>> GetSongs()
        {
            SongDataAccess da = new SongDataAccess();
            return await da.GetListAsync();
        }

        public async Task<List<Song>> GetSongsByAlbum(int albumKey)
        {
            SongDataAccess da = new SongDataAccess();
            return await da.GetListForAlbumAsync(albumKey);
        }

        public async Task<List<Song>> GetSongsByShow(int showKey)
        {
            SongDataAccess da = new SongDataAccess();
            return await da.GetListForShowAsync(showKey);
        }

        public async Task<Song> GetSong(int songKey)
        {
            SongDataAccess da = new SongDataAccess();
            return await da.GetItemAsync(songKey);
        }
    }
}
