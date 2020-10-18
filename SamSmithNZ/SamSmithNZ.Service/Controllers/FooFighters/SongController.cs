using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;

namespace SamSmithNZ.Service.Controllers.FooFighters
{
    [Route("api/foofighters/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongDataAccess _repo;

        public SongController(ISongDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetSongs")]
        public async Task<List<Song>> GetSongs()
        {
            return await _repo.GetListAsync();
        }

        [HttpGet("GetSongsByAlbum")]
        public async Task<List<Song>> GetSongsByAlbum(int albumCode)
        {
            return await _repo.GetListForAlbumAsync(albumCode);
        }

        [HttpGet("GetSongsByShow")]
        public async Task<List<Song>> GetSongsByShow(int showCode)
        {
            return await _repo.GetListForShowAsync(showCode);
        }

        [HttpGet("GetSong")]
        public async Task<Song> GetSong(int songCode)
        {
            return await _repo.GetItemAsync(songCode);
        }
    }
}
