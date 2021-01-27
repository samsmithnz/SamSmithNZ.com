using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.Steam;
using SamSmithNZ.Web.Models.Steam;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class SteamController : Controller
    {
        //https://steamcommunity.com/dev
        //https://developer.valvesoftware.com/wiki/Steam_Web_API#GetPlayerSummaries_.2v0001.29
        //https://portablesteamwebapi.codeplex.com/documentation

        private readonly ISteamServiceAPIClient _ServiceApiClient;
        private readonly IConfiguration _configuration;

        public SteamController(ISteamServiceAPIClient serviceApiClient, IConfiguration configuration)
        {
            _ServiceApiClient = serviceApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string steamID)
        {
            bool useCache = true;
            Player player = await _ServiceApiClient.GetPlayer(steamID, useCache);
            List<Game> games = await _ServiceApiClient.GetPlayerGames(steamID, useCache);

            return View(new IndexViewModel
            {
                SteamId = steamID,
                Player = player,
                Games = games
            });
        }

        public async Task<IActionResult> GameDetails(string steamID, string appID, bool showCompletedAchievements = false)
        {
            bool useCache = true;
            Player player = await _ServiceApiClient.GetPlayer(steamID, useCache);
            GameDetail gameDetail = await _ServiceApiClient.GetGameDetail(steamID, appID, useCache);

            return View(new GameDetailViewModel(gameDetail, showCompletedAchievements)
            {
                SteamId = steamID,
                AppId = appID,
                Player = player
            });
        }

        [HttpGet]
        [HttpPost]
        public IActionResult GameDetailsPost(string steamID, string appID, bool showCompletedAchievements = false)
        {
            return RedirectToAction("GameDetails",
                new
                {
                    steamID = steamID,
                    appID = appID,
                    showCompletedAchievements = showCompletedAchievements
                });
        }

        public IActionResult SteamIsDown()
        {
            return View();
        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }

    }
}