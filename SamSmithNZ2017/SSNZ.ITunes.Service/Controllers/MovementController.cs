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
    public class MovementController : ApiController
    {
        public async Task<List<Movement>> GetMovementsByPlaylist(int playListCode, bool showJustSummary)
        {
            MovementDataAccess da = new MovementDataAccess();
            return await da.GetListAsync(playListCode, showJustSummary);
        }

        public async Task<List<Movement>> GetMovements(bool showJustSummary)
        {
            MovementDataAccess da = new MovementDataAccess();
            return await da.GetListAsync(showJustSummary);
        }

    }
}
