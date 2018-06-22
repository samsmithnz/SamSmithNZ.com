using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
//using SamSmithNZ2015.Core.IntFootball;
//using SamSmithNZ2015.Models.IntFootball;

namespace SamSmithNZ2017.Controllers
{
    public class IntFootballController : Controller
    {
        //
        // GET: /WorldCup/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Tournament(int tournamentCode)
        {
            return View();
        }

        public ActionResult Group(int tournamentCode, int roundNumber, string roundCode)
        {
            return View();
        }

        public ActionResult Playoffs(int tournamentCode, int roundNumber)
        {
            return View();
        }

        public ActionResult Team(int teamCode)
        {
            return View();
        }

        public ActionResult TournamentDetails(int tournamentCode)
        {
            return RedirectToAction("Tournament", new { tournamentCode = tournamentCode });
        }
        
        public ActionResult GroupDetails(int tournamentCode, int roundNumber, bool isLastRound)
        {
            return RedirectToAction("Group", new { tournamentCode = tournamentCode, roundNumber = roundNumber, roundCode = "" });
        }

        public ActionResult PlayoffDetails(int tournamentCode, int roundNumber)
        {
            return RedirectToAction("Playoffs", new { tournamentCode = tournamentCode, roundNumber = roundNumber });
        }

        public ActionResult ELORating(int tournamentCode)
        {
            //SamSmithNZ2015.Core.IntFootball.DataAccess.GameDataAccess da = new SamSmithNZ2015.Core.IntFootball.DataAccess.GameDataAccess();
            //List<Game> gameList = da.GetItems(tournamentCode);

            //ELORatingManager api = new ELORatingManager();
            //List<TeamRating> teamRatingList = api.ProcessTeams(gameList);
            //return View(teamRatingList);

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WCOddsPost(string maxRange = "", bool chkShowActive = true, bool chkShowEliminated = false)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return View();
            }
            else
            {
                return Redirect("https://samsmithnz2015.azurewebsites.net/IntFootball/WCOddsPost?maxRange=" + maxRange + "&chkShowActive=" + chkShowActive + "&chkShowEliminated=" + chkShowEliminated);
            }

            ////return View(WCOdds(maxRange, chkShowActive, chkShowEliminated));
            //return RedirectToAction("Index", "FootballPool", new { maxRange = maxRange, chkShowActive = chkShowActive, chkShowEliminated = chkShowEliminated });
        }

        public ActionResult WCOdds(string maxRange = "", bool showActive = true, bool showEliminated = false, int tournamentCode = 20)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return View();
            }
            else
            {
                return Redirect("https://samsmithnz2015.azurewebsites.net/IntFootball/WCOdds?maxRange=" + maxRange + "&showActive=" + showActive + "&showEliminated=" + showEliminated + "&tournamentCode=" + tournamentCode);
            }

            ////Scrap the odds
            //SamSmithNZ2015.Core.IntFootball.DataAccess.ImportGameOddsDataAccess da = new SamSmithNZ2015.Core.IntFootball.DataAccess.ImportGameOddsDataAccess();
            //List<ImportGameOdds> apiGameResults = new List<ImportGameOdds>();
            ////if (tournamentCode == 20)
            ////{
            ////    WCAPIManager apiManager = new WCAPIManager();
            ////    apiGameResults = apiManager.GetAPIWCOdds();
            ////    //Save the days odds extraction
            ////    if (DateTime.Now.TimeOfDay.Hours < 12)
            ////    {
            ////        foreach (ImportGameOdds game in apiGameResults)
            ////        {
            ////            //make sure we pass the tournament code too (fixes a bug that saves data to a 0 tournament code)
            ////            game.TournamentCode = tournamentCode;
            ////            Console.WriteLine(tournamentCode);
            ////            da.Commit(game);
            ////        }
            ////        da.NormalizeOdds(DateTime.Now);
            ////    }            
            ////}
            //da.AddMissingOdds(DateTime.Now, tournamentCode);
            //apiGameResults = new List<ImportGameOdds>();
            //apiGameResults.AddRange(da.GetItems(tournamentCode, DateTime.Now));

            ////Get odds difference for each team from database
            //SamSmithNZ2015.Core.IntFootball.DataAccess.ImportGameOddsMinMaxDataAccess da2 = new SamSmithNZ2015.Core.IntFootball.DataAccess.ImportGameOddsMinMaxDataAccess();
            //List<ImportGameOddsMinMax> result2 = da2.GetItems(tournamentCode);
            //foreach (ImportGameOdds game in apiGameResults)
            //{
            //    foreach (ImportGameOddsMinMax game2 in result2)
            //    {
            //        if (game.TeamName == game2.TeamName)
            //        {
            //            game.OddsDifference = game2.OddsDifference;
            //            break;
            //        }
            //    }
            //    if (game.OddsMin > 0)
            //    {
            //        game.OddsMin = 1 / game.OddsMin;
            //    }
            //    if (game.OddsMax > 0)
            //    {
            //        game.OddsMax = 1 / game.OddsMax;
            //    }
            //    if (game.OddsMean > 0)
            //    {
            //        game.OddsMean = 1 / game.OddsMean;
            //    }
            //    game.OddsStandardDeviation = game.OddsStandardDeviation;
            //    game.TournamentCode = tournamentCode;
            //}

            ////Sort List by Probability
            //apiGameResults.Sort(delegate (ImportGameOdds p1, ImportGameOdds p2)
            //{
            //    return p2.OddsProbability.CompareTo(p1.OddsProbability);
            //});

            //if (maxRange == "")
            //{
            //    maxRange = "100";
            //}

            ////HtmlString Chance100 = CreateOddsGraphData(1);
            ////HtmlString Chance6 = CreateOddsGraphData(0.06);
            ////HtmlString Chance2 = CreateOddsGraphData(0.02);
            //HtmlString chance = CreateOddsGraphData(Convert.ToDouble(maxRange) / 100, showActive, showEliminated, tournamentCode);

            //return View(new SamSmithNZ2015.Models.IntFootball.OddsViewModel(apiGameResults, chance, maxRange, showActive, showEliminated));
        }

        //From https://github.com/sghall/d3-multi-series-charts       
        public ActionResult WCOddsGraph(int tournamentCode = 20)
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
            {
                return View();
            }
            else
            {
                return Redirect("https://samsmithnz2015.azurewebsites.net/IntFootball/WCOddsGraph?tournamentCode=" + tournamentCode);
            }

            //return View(CreateOddsGraphData(0.02, true, true, tournamentCode));
        }

        //private System.Web.HtmlString CreateOddsGraphData(double oddsLimit, bool chkShowActive, bool chkShowEliminated, int tournamentCode)
        //{
        //    return Redirect("https://samsmithnz2015.azurewebsites.net/IntFootball/Index");

        //    //SamSmithNZ2015.Core.IntFootball.DataAccess.ImportGameOddsDataAccess da = new SamSmithNZ2015.Core.IntFootball.DataAccess.ImportGameOddsDataAccess();
        //    //List<ImportGameOdds> games;
        //    //if (oddsLimit > 0)
        //    //{
        //    //    games = da.GetItems(oddsLimit, chkShowActive, chkShowEliminated, tournamentCode);
        //    //}
        //    //else
        //    //{
        //    //    games = da.GetItems(tournamentCode);
        //    //}
        //    ////Build the header
        //    //StringBuilder headerRow = new StringBuilder();
        //    //headerRow.Append("\"quarter\",");

        //    ////for (int i = 0; i < 32; i++)
        //    ////{
        //    ////    ImportGameOdds item = games[i];
        //    ////}
        //    //foreach (string item in games.Select(x => x.TeamName).Distinct()) //Get a distinct list of team names
        //    //{
        //    //    headerRow.Append("\"");
        //    //    headerRow.Append(item);
        //    //    headerRow.Append("\"");
        //    //    headerRow.Append(",");
        //    //}

        //    ////Build the body
        //    //List<string> rows = new List<string>();
        //    //DateTime currentDate = DateTime.MinValue;
        //    //StringBuilder row = new StringBuilder();

        //    //foreach (ImportGameOdds item in games)
        //    //{
        //    //    if (currentDate != item.Date)
        //    //    {
        //    //        if (currentDate != DateTime.MinValue)
        //    //        {
        //    //            rows.Add(row.ToString().TrimEnd(','));
        //    //            row = new StringBuilder();
        //    //        }
        //    //        row.Append("\"");
        //    //        row.Append(item.Date.ToString("dd-MMM"));
        //    //        row.Append("\"");
        //    //        row.Append(",");
        //    //        currentDate = item.Date;
        //    //    }
        //    //    row.Append("\"");
        //    //    row.Append((item.OddsProbability * 100).ToString("0.00"));
        //    //    row.Append("\"");
        //    //    row.Append(",");
        //    //}
        //    //rows.Add(row.ToString().TrimEnd(','));

        //    ////Join it all together
        //    //List<string> finalCSVRows = new List<string>();
        //    //finalCSVRows.Add(headerRow.ToString().TrimEnd(','));
        //    //finalCSVRows.AddRange(rows);

        //    ////Create the CSV/JS Array
        //    //StringBuilder sbJSArray = new StringBuilder();
        //    //for (int i = 0; i <= finalCSVRows.Count - 1; i++)
        //    //{
        //    //    //sbJSArray.Append("[");
        //    //    sbJSArray.Append(finalCSVRows[i]);
        //    //    //sbJSArray.Append("]");
        //    //    if (i < finalCSVRows.Count - 1)
        //    //    {
        //    //        sbJSArray.Append(",");
        //    //    }
        //    //    else
        //    //    {
        //    //        sbJSArray.Append(Environment.NewLine);
        //    //    }
        //    //}

        //    ////Write out the CSV rows
        //    //StringBuilder csvAnother = new StringBuilder();
        //    //System.Diagnostics.Debug.WriteLine("Starting " + oddsLimit + " max chance");
        //    //foreach (string item in finalCSVRows)
        //    //{
        //    //    System.Diagnostics.Debug.WriteLine(item.Replace("\"", ""));
        //    //    csvAnother.Append(item.Replace("\"", ""));
        //    //    csvAnother.Append(Environment.NewLine);
        //    //}

        //    //string fileName = "Data_" + tournamentCode.ToString("000") + "_" + DateTime.Now.ToString("ddMMM") + "_" + oddsLimit.ToString("0.00").Replace(".", "") + "_" + chkShowActive + "_" + chkShowEliminated + ".csv";
        //    //string path = System.IO.Path.Combine(Server.MapPath("~/Content/D3Content/"), fileName);

        //    //System.IO.StreamWriter objWriter;
        //    //objWriter = new System.IO.StreamWriter(path);
        //    //objWriter.Write(csvAnother.ToString());
        //    //objWriter.Close();

        //    //return new System.Web.HtmlString(fileName);
        //}

    }
}