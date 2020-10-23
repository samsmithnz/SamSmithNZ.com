using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.ITunes;
using SamSmithNZ.Service.DataAccess.ITunes.Interfaces;
using SamSmithNZ.Service.Models.ITunes;

namespace SSNZ.ITunes.Service.Controllers
{
    [Route("api/itunes/[controller]")]
    [ApiController]
    public class TopArtistsController : ControllerBase
    {
        private readonly ITopArtistsDataAccess _repo;

        public TopArtistsController(ITopArtistsDataAccess repo)
        {
            _repo = repo;
        }

        public async Task<List<TopArtists>> GetTopArtistsByPlaylist(int playlistCode, bool showJustSummary)
        {
            return await _repo.GetList(playlistCode, showJustSummary);
        }

        public async Task<List<TopArtists>> GetTopArtistsSummary(bool showJustSummary)
        {
            return await _repo.GetList(showJustSummary);
        }
    }
}
