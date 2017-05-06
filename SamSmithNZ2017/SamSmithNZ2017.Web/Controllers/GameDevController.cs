using System;
using System.Collections.Generic;
using System.Linq;
//using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;
//using SamSmithNZ2015.Models.GameDev;

namespace SamSmithNZ2017.Controllers
{
    public class GameDevController : Controller
    {
        //
        // GET: /GameDev/

        //http://stackoverflow.com/questions/11915/rss-feeds-in-asp-net-mvc
        public ActionResult Index()
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/GameDev/Index");

            //ViewBag.Message = "Game Development Blog!";

            ////const string feedUrl = "http://turnbasedengine.blogspot.com/feeds/posts/default?alt=rss";
            ////SyndicationFeed feed = null;
            ////using (XmlReader reader = XmlReader.Create(feedUrl))
            ////{
            ////    feed = SyndicationFeed.Load(reader);
            ////}
            ////if (feed != null)
            ////{
            ////    SyndicationItem item = feed.Items.First<SyndicationItem>();
            ////    ViewBag.RssItem = item;
            ////}

            //return View();//new BlogItems(feed.Items));
        }

        public ActionResult LevelCreation(int width, int height)
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/GameDev/LevelCreation?width=" + width + "&height=" + height);

            //string fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/Data.xml"));

            //GameDefinition game = GameDefinition.Load(fileContents);
            //List<LevelPiece> levelPieces = Setup.CreateLevelFromPiecePool(game.MissionLevelPieces, width, height, true, false);

            //string result = GameDefinition.OutputMissionLevels(levelPieces, width, height);

            //return View(new LevelCreationModel(result));
        }

        public ActionResult CampaignCreation(int width, int height)
        {
            return Redirect("http://samsmithnz2015.azurewebsites.net/GameDev/CampaignCreation?width=" + width + "&height=" + height);

            //string fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/Data.xml"));

            //GameDefinition game = GameDefinition.Load(fileContents);
            //List<LevelPiece> levelPieces = Setup.CreateLevelFromPiecePool(game.CampaignLevelPieces, width, height, false, true);

            //string result = GameDefinition.OutputCampaignLevels(levelPieces, width, height);

            //return View(new LevelCreationModel(result));
        }

    }
}
