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
    public class PlayerGamesController : ControllerBase
    {
        // GET
        [HttpGet("{id}")]
        public async Task<List<Game>> GetPlayer(string steamID)
        {
            PlayerGamesDA da = new PlayerGamesDA();
            return await da.GetDataAsync(steamID);
        }
    }
}
