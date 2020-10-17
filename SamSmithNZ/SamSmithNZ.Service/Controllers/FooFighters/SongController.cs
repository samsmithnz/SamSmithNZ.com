using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;

namespace SamSmithNZ.Service.Controllers.FooFighters
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongDataAccess _repo;

        public SongController(ISongDataAccess repo)
        {
            _repo = repo;
        }

        public async Task<List<Song>> GetSongs()
        {
            return await _repo.GetListAsync();
        }

        public async Task<List<Song>> GetSongsByAlbum(int albumCode)
        {
            return await _repo.GetListForAlbumAsync(albumCode);
        }

        public async Task<List<Song>> GetSongsByShow(int showCode)
        {
            return await _repo.GetListForShowAsync(showCode);
        }

        public async Task<Song> GetSong(int songCode)
        {
            return await _repo.GetItemAsync(songCode);
        }
    }
}
