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
    public class ArtistController : ControllerBase
    {
        private readonly IArtistDataAccess _repo;

        public ArtistController(IArtistDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetArtists")]
        public async Task<List<Artist>> GetArtists(bool includeAllItems = false)
        {
            return await _repo.GetList(includeAllItems);
        }
    }
}
