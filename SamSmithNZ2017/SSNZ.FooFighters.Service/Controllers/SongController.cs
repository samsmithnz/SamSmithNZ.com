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

        public async Task<List<Song>> GetSongsByAlbum(int albumCode)
        {
            SongDataAccess da = new SongDataAccess();
            return await da.GetListForAlbumAsync(albumCode);
        }

        public async Task<List<Song>> GetSongsByShow(int showCode)
        {
            SongDataAccess da = new SongDataAccess();
            return await da.GetListForShowAsync(showCode);
        }

        public async Task<Song> GetSong(int songCode)
        {
            SongDataAccess da = new SongDataAccess();
            return await da.GetItemAsync(songCode);
        }
    }
}
