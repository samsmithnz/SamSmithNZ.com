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
    public class TrackController : ControllerBase
    {
        private readonly ITrackDataAccess _repo;

        public TrackController(ITrackDataAccess repo)
        {
            _repo = repo;
        }

        public async Task<List<Track>> GetTracks(int playlistCode, bool showJustSummary)
        {
            return await _repo.GetList(playlistCode, showJustSummary);
        }

        //public async Task<Track> GetTrack(int playlistCode, bool showJustSummary, String trackName)
        //{
        //    TrackDataAccess da = new TrackDataAccess();
        //    return await da.GetItemAsync(playlistCode, showJustSummary, trackName);
        //}
    }
}
