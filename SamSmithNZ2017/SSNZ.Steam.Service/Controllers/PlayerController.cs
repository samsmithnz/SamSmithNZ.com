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
    public class PlayerController : ApiController
    {
        public async Task<Player> GetPlayer(string steamID)
        {
            PlayerDA da = new PlayerDA();
            return await da.GetDataAsync(steamID);
        }
    }
}
