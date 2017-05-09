using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSNZ.Steam.CoreData;
using SSNZ.Steam.CoreModels;

namespace SSNZ.Steam.CoreService.Controllers
{
    public class PlayerGamesController : Controller
    {
        public async Task<List<Game>> GetPlayer(string steamID)
        {
            PlayerGamesDA da = new PlayerGamesDA();
            return await da.GetDataAsync(steamID);
        }
    }
}
