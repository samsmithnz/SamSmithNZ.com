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
    public class TopArtistsController : ApiController
    {
        public async Task<List<TopArtists>> GetTopArtistsByPlaylist(int playlistCode, Boolean showJustSummary)
        {
            TopArtistsDataAccess da = new TopArtistsDataAccess();
            return await da.GetListAsync(playlistCode, showJustSummary);
        }

        public async Task<List<TopArtists>> GetTopArtistsSummary(Boolean showJustSummary)
        {
            TopArtistsDataAccess da = new TopArtistsDataAccess();
            return await da.GetListAsync(showJustSummary);
        }
    }
}
