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
    public class PlaylistController : ApiController
    {
        public async Task<List<Playlist>> GetPlaylists()
        {
            PlaylistDataAccess da = new PlaylistDataAccess();
            return await da.GetListAsync();
        }

        public async Task<Playlist> GetPlaylist(int playlistCode)
        {
            PlaylistDataAccess da = new PlaylistDataAccess();
            return await da.GetItemAsync(playlistCode);
        }
        
    }
}
