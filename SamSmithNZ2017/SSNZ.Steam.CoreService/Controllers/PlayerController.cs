using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSNZ.Steam.CoreData;
using SSNZ.Steam.CoreModels;

namespace SSNZ.Steam.CoreService.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        // GET api/values/5
        [HttpGet("{steamID}")]
        public async Task<Player> GetPlayer(string steamID)
        {
            PlayerDA da = new PlayerDA();
            return await da.GetDataAsync(steamID);
        }
    }
}
