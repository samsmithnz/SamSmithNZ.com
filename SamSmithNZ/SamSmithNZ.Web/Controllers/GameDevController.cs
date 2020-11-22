using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Web.Models.GameDev;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class GameDevController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public GameDevController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LevelCreation(int width = 8, int height = 8)
        {
            string fileContents = System.IO.File.ReadAllText(_env.WebRootPath + @"/GameDev/Data.xml");

            GameDefinition game = GameDefinition.Load(fileContents);
            List<LevelPiece> levelPieces = Setup.CreateLevelFromPiecePool(game.MissionLevelPieces, width, height, true, false);

            string result = GameDefinition.OutputMissionLevels(levelPieces, width, height);

            return View(new LevelCreationModel(result));
        }

        public ActionResult CampaignCreation(int width = 4, int height = 4)
        {
            string fileContents = System.IO.File.ReadAllText(_env.WebRootPath + @"/GameDev/Data.xml");

            GameDefinition game = GameDefinition.Load(fileContents);
            List<LevelPiece> levelPieces = Setup.CreateLevelFromPiecePool(game.CampaignLevelPieces, width, height, false, true);

            string result = GameDefinition.OutputCampaignLevels(levelPieces, width, height);

            return View(new LevelCreationModel(result));
        }

    }
}
