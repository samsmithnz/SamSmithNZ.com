using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;

namespace SamSmithNZ.Service.Controllers.GuitarTab
{
    [Route("api/guitartab/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumDataAccess _repo;

        public AlbumController(IAlbumDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAlbums")]
        public async Task<List<Album>> GetAlbums(bool isAdmin = false)
        {
            return await _repo.GetList(isAdmin);
        }

        [HttpGet("GetAlbum")]
        public async Task<Album> GetAlbum(int albumCode, bool isAdmin = false)
        {
            return await _repo.GetItem(albumCode, isAdmin);
        }

        [HttpPost("SaveAlbum")]
        public async Task<Album> SaveAlbum(Album item)
        {
            return await _repo.SaveItem(item);
        }
    }
}
