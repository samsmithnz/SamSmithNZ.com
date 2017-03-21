using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using SamSmithNZ2015.Core.FootballPool;
//using SamSmithNZ2015.Core.FootballPool.DataAccess;
//using SamSmithNZ2015.Models.FootballPool;

namespace SamSmithNZ2017.Controllers
{

    public class FootballPoolController : Controller
    {
        //
        // GET: /FootballPool/

        public ActionResult AccountingSummary(short yearCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //AccountingSummaryDataAccess DA = new AccountingSummaryDataAccess();
            //IList<SamSmithNZ2015.Core.FootballPool.AccountingSummary> AccountingSummaryList = DA.GetItems(yearCode);

            //return View(AccountingSummaryList);
        }

        public ActionResult AccountingTransactions(short yearCode, short playerCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //YearWeekPlayerBase yearWeekPlayer = new YearWeekPlayerBase(yearCode, 0, playerCode);

            //AccountingTransactionDataAccess DA = new AccountingTransactionDataAccess();
            //IList<AccountingTransaction> AccountingTransactionList;
            //if (playerCode == 0)
            //{
            //    AccountingTransactionList = DA.GetItems(yearCode);
            //}
            //else
            //{
            //    AccountingTransactionList = DA.GetItems(yearCode, playerCode);
            //}

            //return View(new AccountingTransactionsViewModel(yearWeekPlayer, AccountingTransactionList));
        }

        //To get the data for the page
        public ActionResult AddAccountingTransaction(short yearCode, short weekCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //YearWeekPlayerBase yearWeekPlayer = new YearWeekPlayerBase(yearCode, weekCode, 0);

            //PlayerDataAccess DA = new PlayerDataAccess();
            //IList<Player> playerList = DA.GetItems(yearCode);

            //AccountingTransactionTypeDataAccess DA2 = new AccountingTransactionTypeDataAccess();
            //IList<AccountingTransactionType> typeList = DA2.GetItems();

            //return View(new AddAccountingTransactionViewModel(yearWeekPlayer, playerList, typeList));
        }

        [HttpPost]
        public ActionResult AddAccountingTransaction(short yearCode, short weekCode, string cboPlayers, decimal? txtAmount, string cboTypes)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //AccountingTransaction item = new AccountingTransaction();
            //item.YearCode = yearCode;
            //if (weekCode == 0)
            //{
            //    CurrentSettingsDataAccess DA = new CurrentSettingsDataAccess();
            //    CurrentSettings currentSettings = DA.GetItem();
            //    item.WeekCode = currentSettings.CurrentWeekCode;
            //}
            //else
            //{
            //    item.WeekCode = weekCode;
            //}
            ////Load the values from the dropdown lists and textbox and add the transaction
            //if (cboPlayers != null && txtAmount != null && cboTypes != null)
            //{
            //    short playerCode;
            //    if (short.TryParse(cboPlayers, out playerCode) == true)
            //    {
            //        item.PlayerCode = playerCode;
            //    }

            //    decimal amount;
            //    if (Decimal.TryParse(txtAmount.ToString(), out amount) == true)
            //    {
            //        item.Amount = amount;
            //    }

            //    short typeCode;
            //    if (short.TryParse(cboTypes, out typeCode) == true)
            //    {
            //        item.TransactionTypeCode = typeCode;
            //    }

            //    AccountingTransactionDataAccess DA = new AccountingTransactionDataAccess();
            //    DA.AddNewAccountingTransaction(item);

            //    return RedirectToAction("AccountingTransactions", "FootballPool", new { yearCode = yearCode, playerCode = playerCode });
            //}
            //else
            //{
            //    return View();
            //}

        }

        [ChildActionOnly]
        public ActionResult FootballPoolSideBar(short yearCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //CurrentSettingsDataAccess DA2 = new CurrentSettingsDataAccess();
            //CurrentSettings currentSettings = DA2.GetItem();
            //if (yearCode == 0)
            //{
            //    yearCode = currentSettings.CurrentYearCode;
            //}
            //short weekCode = currentSettings.CurrentWeekCode;

            //WeekDataAccess DA = new WeekDataAccess();
            //IList<Week> WeekList = DA.GetItems(yearCode);

            //// Return partial view
            //return PartialView(new SamSmithNZ2015.Models.FootballPool.SidebarViewModel(yearCode, weekCode, WeekList));
        }

        [ChildActionOnly]
        public ActionResult HeaderStatus()
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //CurrentSettingsDataAccess DA = new CurrentSettingsDataAccess();
            //CurrentSettings currentSettings = DA.GetItem();

            //// Return partial view
            //return PartialView(new SamSmithNZ2015.Models.FootballPool.YearWeekPlayerBase(currentSettings.CurrentYearCode, currentSettings.CurrentWeekCode, 0));
        }

        public ActionResult Helmets()
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //TeamDataAccess DA = new TeamDataAccess();
            //IList<Team> TeamList = DA.GetItems();

            //return View(TeamList);
        }

        [HttpPost]
        public ActionResult MoveToNextWeekAction(short yearCode, short weekCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //MoveToNextWeekDataAccess DA = new MoveToNextWeekDataAccess();
            //if (DA.MoveToNextWeek(yearCode) == true)
            //{
            //    CurrentSettingsDataAccess DA2 = new CurrentSettingsDataAccess();
            //    CurrentSettings currentSettings = DA2.GetItem();
            //    yearCode = currentSettings.CurrentYearCode;
            //    weekCode = currentSettings.CurrentWeekCode;
            //}

            //return RedirectToAction("Index", "FootballPool", new { yearCode = yearCode, weekCode = weekCode, sortOrder = 0 });

            //  return View();
        }

        public ActionResult MoveToNextWeek(short yearCode, short weekCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //return View(new YearWeekPlayerBase(yearCode, weekCode, 0));
        }

        public ActionResult PickSheet(short yearCode, short weekCode, short playerCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //Boolean IncludeTeamRecords = true;

            ////if it's the first week, don't show the team records - because they are all 0-0-0, so there is no point.
            //if (weekCode <= 1)
            //{
            //    IncludeTeamRecords = false;
            //}
            ////If the player code isn't set to a real player
            //if (playerCode == 0)
            //{
            //    playerCode = 1;
            //}
            //PickDataAccess DA = new PickDataAccess();
            //IList<Pick> pickList = DA.GetItems(yearCode, weekCode, playerCode, IncludeTeamRecords);

            //PlayerDataAccess DA2 = new PlayerDataAccess();
            //IList<Player> playerList = DA2.GetItems(yearCode);

            //return View(new PickSheetViewModel(playerList, pickList));
        }

        public ActionResult PotentialWinners(short yearCode, short weekCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //PotentialWinnersDataAccess DA = new PotentialWinnersDataAccess();
            //IList<PotentialWinners> potentialWinnersList = DA.GetItems(yearCode, weekCode);

            //return View(new PotentialWinnersViewModel(potentialWinnersList));
        }

        public ActionResult SamsModel(short yearCode, short weekCode, short playerCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //SamsModelDataAccess DA = new SamsModelDataAccess();
            //IList<SamsModel> SamModelsList = DA.GetItems(yearCode, weekCode, playerCode, false);

            //StatisticsBySpreadDataAccess DA2 = new StatisticsBySpreadDataAccess();
            //IList<StatisticsBySpread> StatisticsBySpreadList = DA2.GetItems();

            //List<TeamHeadToHead> teamHeadToHeadStatsList = new List<TeamHeadToHead>();
            //foreach (SamsModel item in SamModelsList)
            //{
            //    TeamHeadToHeadDataAccess DA3 = new TeamHeadToHeadDataAccess();
            //    short favTeamCode = 0;
            //    short underdogTeamCode = 0;
            //    if (item.FavTeamCode == item.HomeTeamCode)
            //    {
            //        favTeamCode = item.HomeTeamCode;
            //        underdogTeamCode = item.AwayTeamCode;
            //    }
            //    else
            //    {
            //        favTeamCode = item.AwayTeamCode;
            //        underdogTeamCode = item.HomeTeamCode;
            //    }
            //    IList<TeamHeadToHead> SamModels2List = DA3.GetItems(favTeamCode, underdogTeamCode);
            //    if (SamModels2List.Count > 0)
            //    {
            //        teamHeadToHeadStatsList.Add(SamModels2List[0]);
            //    }
            //}

            //return View(new SamsModelViewModel(SamModelsList, StatisticsBySpreadList, teamHeadToHeadStatsList));
        }

        public ActionResult SeasonStatistics()
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //StatisticsByPicksDataAccess DA = new StatisticsByPicksDataAccess();
            //IList<StatisticsByPicks> statisticsByPicksList = DA.GetItems();
            //StatisticsByWinsDataAccess DA2 = new StatisticsByWinsDataAccess();
            //IList<StatisticsByWins> statisticsByWinsList = DA2.GetItems();
            //StatisticsByWinsDetailedDataAccess DA3 = new StatisticsByWinsDetailedDataAccess();
            //IList<StatisticsByWinsDetailed> statisticsByWinsDetailedList = DA3.GetItems();

            //return View(new SeasonStatisticsViewModel(statisticsByPicksList, statisticsByWinsList, statisticsByWinsDetailedList));
        }

        public ActionResult Index(short? yearCode, short? weekCode, short? sortOrder, bool? isAdmin)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //short finalYearCode;
            //short finalWeekCode;
            //short finalSortOrder;
            //bool finalIsAdmin;

            //if (yearCode == null || weekCode == null || yearCode == 0 || weekCode == 0)
            //{
            //    CurrentSettingsDataAccess DA2 = new CurrentSettingsDataAccess();
            //    CurrentSettings currentSettings = DA2.GetItem();
            //    finalYearCode = currentSettings.CurrentYearCode;
            //    finalWeekCode = currentSettings.CurrentWeekCode;
            //}
            //else
            //{
            //    finalYearCode = short.Parse(yearCode.ToString());
            //    finalWeekCode = short.Parse(weekCode.ToString());
            //}
            //if (sortOrder == null)
            //{
            //    finalSortOrder = 0;
            //}
            //else
            //{
            //    finalSortOrder = short.Parse(sortOrder.ToString());
            //}
            //if (isAdmin == null)
            //{
            //    finalIsAdmin = false;
            //}
            //else
            //{
            //    finalIsAdmin = bool.Parse(isAdmin.ToString());
            //}

            //SummaryDataAccess DA = new SummaryDataAccess();
            //IList<Summary> SummaryList = DA.GetItems(finalYearCode, finalWeekCode, finalSortOrder);

            //SummaryViewModel pageModel = new SummaryViewModel(finalYearCode, finalWeekCode, SummaryList);

            //return View(pageModel);
        }

        public ActionResult WeekSetup(short yearCode, short weekCode)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //WeekStatusDataAccess DA = new WeekStatusDataAccess();
            //WeekStatus weekStatus = DA.GetItem(yearCode, weekCode);

            //WeekTemplateDataAccess DA2 = new WeekTemplateDataAccess();
            //IList<WeekTemplate> WeekTemplateList = DA2.GetItems(yearCode, weekCode);

            //TeamDataAccess DA3 = new TeamDataAccess();
            //IList<Team> teamList = DA3.GetItems();

            //return View(new WeekSetupViewModel(weekStatus, WeekTemplateList, teamList));
        }

        public ActionResult SparklineTest()
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            ////PlayerSummaryDataAccess DA = new PlayerSummaryDataAccess();
            ////List<PlayerSummary> SummaryList = DA.GetItems(2014, 1);

            //List<PlayerSummary> SummaryList = new List<PlayerSummary>();
            //SummaryList.Add(new PlayerSummary(1, 1, 1));
            //SummaryList.Add(new PlayerSummary(2, 1, 5));
            //SummaryList.Add(new PlayerSummary(3, 1, 2));
            //SummaryList.Add(new PlayerSummary(4, 1, 9));
            //SummaryList.Add(new PlayerSummary(5, 1, 1));
            //SummaryList.Add(new PlayerSummary(6, 1, 9));
            //SummaryList.Add(new PlayerSummary(7, 1, 5));

            //return View(SummaryList);
        }

        [HttpPost]
        public ActionResult SaveWeekSetup(short yearCode, short weekCode, decimal? txtDonutCost, FormCollection post)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //WeekTemplateDataAccess DA = new WeekTemplateDataAccess();
            //IList<WeekTemplate> WeekTemplateList = DA.GetItems(yearCode, weekCode);

            //foreach (WeekTemplate item in WeekTemplateList)
            //{
            //    if (post["cboTeams_home_" + item.RecordID.ToString()] != null)
            //    {
            //        short homeTeamCode = -1;
            //        if (short.TryParse(post["cboTeams_home_" + item.RecordID.ToString()], out homeTeamCode) == true)
            //        {
            //            item.HomeTeamCode = homeTeamCode;
            //        }
            //        else
            //        {
            //            item.HomeTeamCode = -1;
            //        }
            //        short awayTeamCode = -1;
            //        if (short.TryParse(post["cboTeams_away_" + item.RecordID.ToString()], out awayTeamCode) == true)
            //        {
            //            item.AwayTeamCode = awayTeamCode;
            //        }
            //        else
            //        {
            //            item.AwayTeamCode = -1;
            //        }
            //        Boolean? homeTeamIsFavTeam = true;
            //        homeTeamIsFavTeam = (bool)ValueProvider.GetValue("chkHomeTeamIsFavTeam_" + item.RecordID.ToString()).ConvertTo(typeof(bool));

            //        if (homeTeamIsFavTeam == null)
            //        {
            //            item.FavTeamCode = -1;
            //        }
            //        else if (homeTeamIsFavTeam == true)
            //        {
            //            item.FavTeamCode = homeTeamCode;
            //        }
            //        else
            //        {
            //            item.FavTeamCode = awayTeamCode;
            //        }

            //        // if (Boolean.TryParse(post["chkHomeTeamIsFavTeam_" + item.RecordID.ToString()].Contains("true"), out homeTeamIsFavTeam) == true)
            //        //{
            //        //    if (homeTeamIsFavTeam == true)
            //        //    {
            //        //        item.FavTeamCode = homeTeamCode;
            //        //    }
            //        //    else
            //        //    {
            //        //        item.FavTeamCode = awayTeamCode;
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    item.FavTeamCode = -1;
            //        //}
            //        //short homeTeamCode = -1;
            //        //if (short.TryParse( post["optAway_" + item.RecordID.ToString()], out homeTeamCode) == true)
            //        //{
            //        //    item.HomeTeamCode = homeTeamCode;
            //        //}
            //        DateTime gameTime;
            //        if (DateTime.TryParse(post["txtGameDateTime_" + item.RecordID.ToString()], out gameTime) == true)
            //        {
            //            item.GameTime = gameTime;
            //        }
            //        decimal spread = 0;
            //        if (decimal.TryParse(post["txtSpread_" + item.RecordID.ToString()], out spread) == true)
            //        {
            //            item.Spread = spread;
            //        }
            //        else
            //        {
            //            item.Spread = 0m;
            //        }
            //        short homeTeamResult = -1;
            //        if (short.TryParse(post["txtHomeTeamResult_" + item.RecordID.ToString()], out homeTeamResult) == false)
            //        {
            //            homeTeamResult = -1;
            //        }
            //        item.HomeTeamResult = homeTeamResult;

            //        short awayTeamResult = -1;
            //        if (short.TryParse(post["txtAwayTeamResult_" + item.RecordID.ToString()], out awayTeamResult) == false)
            //        {
            //            awayTeamResult = -1;
            //        }
            //        item.AwayTeamResult = awayTeamResult;

            //        item.FavTeamWonGame = WeekTemplate.CalculateWhoWonGame(item.HomeTeamResult, item.AwayTeamResult, item.FavTeamCode, item.HomeTeamCode, item.AwayTeamCode, item.Spread);

            //        ////Work out if the fav team beat the spread.
            //        //if (item.HomeTeamResult >= 0 & item.AwayTeamResult >= 0)
            //        //{
            //        //        //If the home team was the favorite and beat the spread, then the favorite won the game
            //        //        if (item.FavTeamCode == item.HomeTeamCode && ((item.HomeTeamResult + item.Spread) - item.AwayTeamResult) > 0)
            //        //        {
            //        //            item.FavTeamWonGame = 1;
            //        //        }
            //        //        //If the Away team was the favorite and beat the spread, then the favorite won the game
            //        //        //if ((item.HomeTeamResult - (item.AwayTeamResult + item.Spread)) <= 0)
            //        //        else if (item.FavTeamCode == item.AwayTeamCode && ((item.AwayTeamResult + item.Spread) - item.HomeTeamResult) > 0)
            //        //        {
            //        //            item.FavTeamWonGame = 1;
            //        //        }
            //        //        else //otherwise, the underdog won the game
            //        //        {
            //        //            item.FavTeamWonGame = 0;
            //        //        }

            //        //if (item.FavTeamCode == item.HomeTeamCode)
            //        //{
            //        //    //If the home team was the favorite and beat the spread, then the favorite won the game
            //        //    if (((item.HomeTeamResult + item.Spread) - item.AwayTeamResult) > 0)
            //        //    {
            //        //        item.FavTeamWonGame = 1;
            //        //    }
            //        //    else //otherwise, the underdog won the game
            //        //    {
            //        //        item.FavTeamWonGame = 0;
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    //If the Away team was the favorite and beat the spread, then the favorite won the game
            //        //    //if ((item.HomeTeamResult - (item.AwayTeamResult + item.Spread)) <= 0)
            //        //    if (((item.AwayTeamResult + item.Spread) - item.HomeTeamResult) > 0)
            //        //    {
            //        //        item.FavTeamWonGame = 1;
            //        //    }
            //        //    else//otherwise, the underdog won the game
            //        //    {
            //        //        item.FavTeamWonGame = 0;
            //        //    }
            //        //}
            //        //}
            //        //else
            //        //{
            //        //    item.FavTeamWonGame = -1;
            //        //}
            //    }
            //}
            //DA.Commit(WeekTemplateList);

            ////Now save the donut cost for the week
            //decimal donutCost;
            //Decimal.TryParse(txtDonutCost.ToString(), out donutCost);

            //WeekStatusDataAccess DA2 = new WeekStatusDataAccess();
            //DA2.Commit(yearCode, weekCode, donutCost);

            //return RedirectToAction("Index", "FootballPool", new { yearCode = yearCode, weekCode = weekCode, sortOrder = 0 });

        }

        [HttpPost]
        public ActionResult OddsPost(string maxRange = "", bool chkShowActive = true, bool chkShowEliminated = false)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            ////return View(WCOdds(maxRange, chkShowActive, chkShowEliminated));
            //return RedirectToAction("Index", "FootballPool", new { maxRange = maxRange, chkShowActive = chkShowActive, chkShowEliminated = chkShowEliminated });
        }

        public ActionResult OddsBackup()
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //List<string> debugList = new List<string>();
            //int yearCode = 2014;

            ////Scrape the odds
            //SamSmithNZ2015.Core.FootballPool.DataAccess.FBImportGameOddsDataAccess da = new SamSmithNZ2015.Core.FootballPool.DataAccess.FBImportGameOddsDataAccess();
            //List<FBAPIImportGameOdds> apiGameResults = new List<FBAPIImportGameOdds>();
            //if (yearCode == 2014)
            //{
            //    FBAPIManager apiManager = new FBAPIManager();
            //    apiGameResults = apiManager.GetAPIFBOdds();
            //    debugList.Add(apiGameResults.Count.ToString() + " items found in API");
            //    //Save the days odds extraction
            //    debugList.Add("Time:" + DateTime.Now.TimeOfDay.Hours);
            //    //if (DateTime.Now.TimeOfDay.Hours < 12)
            //    //{
            //    foreach (FBAPIImportGameOdds game in apiGameResults)
            //    {
            //        ////make sure we pass the tournament code too (fixes a bug that saves data to a 0 tournament code)
            //        game.YearCode = yearCode;
            //        //Console.WriteLine(tournamentCode);
            //        da.Commit(game);
            //        debugList.Add("Saved game " + game.TeamName + ", odds: " + game.OddsProbability.ToString() + ", on the date: " + game.Date.ToString());
            //    }
            //    da.NormalizeOdds(DateTime.Now);
            //    debugList.Add("Odds Normalized");
            //    //}
            //}
            //da.AddMissingOdds(DateTime.Now, yearCode);
            //debugList.Add("Missing Odds added");
            //apiGameResults = new List<FBAPIImportGameOdds>();
            //apiGameResults.AddRange(da.GetItems(2014, DateTime.Now));
            //debugList.Add(apiGameResults.Count.ToString() + " items found in database");

            //return View(debugList);
        }

        public ActionResult Odds(string maxRange = "", bool showActive = true, bool showEliminated = false, int yearCode = 2014, bool top8Items = false)
        {
            return Redirect("http://samsmithnz.com/FootballPool/Index");

            //List<string> debugList = new List<string>();

            ////Scrape the odds
            //SamSmithNZ2015.Core.FootballPool.DataAccess.FBImportGameOddsDataAccess da = new SamSmithNZ2015.Core.FootballPool.DataAccess.FBImportGameOddsDataAccess();
            //List<FBAPIImportGameOdds> apiGameResults = new List<FBAPIImportGameOdds>();
            ////if (yearCode == 2014)
            ////{
            ////    FBAPIManager apiManager = new FBAPIManager();
            ////    apiGameResults = apiManager.GetAPIFBOdds();
            ////    debugList.Add(apiGameResults.Count.ToString() + " items found in API");
            ////    //Save the days odds extraction
            ////    debugList.Add("Time:" + DateTime.Now.TimeOfDay.Hours);
            ////    //if (DateTime.Now.TimeOfDay.Hours < 12)
            ////    //{
            ////        foreach (FBAPIImportGameOdds game in apiGameResults)
            ////        {
            ////            ////make sure we pass the tournament code too (fixes a bug that saves data to a 0 tournament code)
            ////            game.YearCode = yearCode;
            ////            //Console.WriteLine(tournamentCode);
            ////            da.Commit(game);
            ////            debugList.Add("Saved game " + game.TeamName + ", odds: " + game.OddsProbability.ToString() + ", on the date: " + game.Date.ToString());
            ////        }
            ////        da.NormalizeOdds(DateTime.Now);
            ////        debugList.Add("Odds Normalized");
            ////    //}
            ////}
            ////da.AddMissingOdds(DateTime.Now, yearCode);
            ////debugList.Add("Missing Odds added");
            //apiGameResults = new List<FBAPIImportGameOdds>();
            //apiGameResults.AddRange(da.GetItems(2014, DateTime.Now));
            //debugList.Add(apiGameResults.Count.ToString() + " items found in database");

            //////Get odds difference for each team from database
            ////SamSmithNZ2015.Core.IntFootball.DataAccess.ImportGameOddsMinMaxDataAccess da2 = new SamSmithNZ2015.Core.IntFootball.DataAccess.ImportGameOddsMinMaxDataAccess();
            ////List<ImportGameOddsMinMax> result2 = da2.GetItems(tournamentCode);
            ////foreach (ImportGameOdds game in apiGameResults)
            ////{
            ////    foreach (ImportGameOddsMinMax game2 in result2)
            ////    {
            ////        if (game.TeamName == game2.TeamName)
            ////        {
            ////            game.OddsDifference = game2.OddsDifference;
            ////            break;
            ////        }
            ////    }
            ////    if (game.OddsMin > 0)
            ////    {
            ////        game.OddsMin = 1 / game.OddsMin;
            ////    }
            ////    if (game.OddsMax > 0)
            ////    {
            ////        game.OddsMax = 1 / game.OddsMax;
            ////    }
            ////    if (game.OddsMean > 0)
            ////    {
            ////        game.OddsMean = 1 / game.OddsMean;
            ////    }
            ////    game.OddsStandardDeviation = game.OddsStandardDeviation;
            ////    game.TournamentCode = tournamentCode;
            ////}

            ////Sort List by Probability
            //apiGameResults.Sort(delegate(FBAPIImportGameOdds p1, FBAPIImportGameOdds p2)
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
            //HtmlString chance = CreateOddsGraphData(Convert.ToDouble(maxRange) / 100, showActive, showEliminated, yearCode, top8Items);

            //return View(new SamSmithNZ2015.Models.FootballPool.OddsViewModel(apiGameResults, chance, maxRange, showActive, showEliminated, debugList));
        }

        //private System.Web.HtmlString CreateOddsGraphData(double oddsLimit, bool chkShowActive, bool chkShowEliminated, int yearCode, bool top10Items)
        //{
        //    return Redirect("http://samsmithnz.com/FootballPool/Index");

        //    //SamSmithNZ2015.Core.FootballPool.DataAccess.FBImportGameOddsDataAccess da = new SamSmithNZ2015.Core.FootballPool.DataAccess.FBImportGameOddsDataAccess();
        //    //List<FBAPIImportGameOdds> games = new List<FBAPIImportGameOdds>();
        //    //if (oddsLimit > 0)
        //    //{
        //    //    games = da.GetItems(oddsLimit, chkShowActive, chkShowEliminated, yearCode, top10Items);
        //    //}
        //    //else if (top10Items == true)
        //    //{
        //    //    games = da.GetItems(oddsLimit, chkShowActive, chkShowEliminated, yearCode, top10Items);
        //    //}
        //    //else
        //    //{
        //    //    games = da.GetItems(yearCode);
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

        //    //foreach (FBAPIImportGameOdds item in games)
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

        //    //string fileName = "Data_" + yearCode.ToString("000") + "_" + DateTime.Now.ToString("ddMMM") + "_" + oddsLimit.ToString("0.00").Replace(".", "") + "_" + chkShowActive + "_" + chkShowEliminated + ".csv";
        //    //string path = System.IO.Path.Combine(Server.MapPath("~/Content/D3Content/"), fileName);

        //    //System.IO.StreamWriter objWriter;
        //    //objWriter = new System.IO.StreamWriter(path);
        //    //objWriter.Write(csvAnother.ToString());
        //    //objWriter.Close();

        //    //return new System.Web.HtmlString(fileName);
        //}


    }
}
