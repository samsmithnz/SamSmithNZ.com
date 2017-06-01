using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SSNZ.ITunes.Data;
using SSNZ.ITunes.Models;

namespace SSNZ.ITunes.Service.Controllers
{
    public class TrackController : ApiController
    {
        public async Task<List<Track>> GetTracks(int playlistCode, Boolean showJustSummary)
        {
            TrackDataAccess da = new TrackDataAccess();
            return await da.GetListAsync(playlistCode, showJustSummary);
        }

        public async Task<Track> GetTrack(int playlistCode, Boolean showJustSummary, String trackName)
        {
            TrackDataAccess da = new TrackDataAccess();
            return await da.GetItemAsync(playlistCode, showJustSummary, trackName);
        }
    }
}
