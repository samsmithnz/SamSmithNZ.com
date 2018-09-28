using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSNZ.Steam2019.Service.DataAccess;
using SSNZ.Steam2019.Service.Models;

namespace SSNZ.Steam2019.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        // GET
        [HttpGet]
        public async Task<Player> GetPlayer(string steamID)
        {
            PlayerDA da = new PlayerDA();
            return await da.GetDataAsync(steamID);
        }
    }
}
