using System;
using System.Collections.Generic;
using System.Linq;
//using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using SamSmithNZ2017.Models.GameDev;

namespace SamSmithNZ2017.Controllers
{
    public class GameDevController : Controller
    {
        //
        // GET: /GameDev/

        public ActionResult Index()
        {
            ViewBag.Message = "Game Development Blog!";

            return View();
        }

        public ActionResult LevelCreation(int width, int height)
        {

            string fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/Data.xml"));

            GameDefinition game = GameDefinition.Load(fileContents);
            List<LevelPiece> levelPieces = Setup.CreateLevelFromPiecePool(game.MissionLevelPieces, width, height, true, false);

            string result = GameDefinition.OutputMissionLevels(levelPieces, width, height);

            return View(new LevelCreationModel(result));
        }

        public ActionResult CampaignCreation(int width, int height)
        {

            string fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/Data.xml"));

            GameDefinition game = GameDefinition.Load(fileContents);
            List<LevelPiece> levelPieces = Setup.CreateLevelFromPiecePool(game.CampaignLevelPieces, width, height, false, true);

            string result = GameDefinition.OutputCampaignLevels(levelPieces, width, height);

            return View(new LevelCreationModel(result));
        }

    }
}
