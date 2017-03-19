using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Service.Controllers
{
    public class PlayerGamesController : ApiController
    {
        public async Task<List<Game>> GetPlayer(string steamID)
        {
            PlayerGamesDA da = new PlayerGamesDA();
            return await da.GetDataAsync(steamID);
        }
    }
}
