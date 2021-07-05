using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for Games.xaml
    /// </summary>
    public partial class GamesMigration : Window
    {
        private int _tournamentCode;
        private readonly IConfigurationRoot _configuration;
        private List<Game> _Games;
        private List<Goal> _Goals;

        public GamesMigration()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Main>(true);
            _configuration = config.Build();
        }

        public async Task<bool> ShowForm(int tournamentCode)
        {
            _tournamentCode = tournamentCode;

            TeamDataAccess daTeam = new(_configuration);
            List<Team> teams = await daTeam.GetList();
            PlayerDataAccess daPlayer = new(_configuration);
            List<Player> players = await daPlayer.GetPlayersByTournament(_tournamentCode);

            string url = "https://en.wikipedia.org/wiki/UEFA_Euro_2016#Group_A";
            HtmlWeb web = new();
            HtmlDocument doc = web.Load(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(@"//*[@class=""footballbox""]");

            _Games = new();
            _Goals = new();
            int gameCount = 0;
            string roundCode = "A";
            int groupGames = 6;
            int roundGames = 6 * 6;
            int roundNumber = 1;
            int top16Count = 0;
            int qfCount = 0;
            int sfCount = 0;

            foreach (HtmlNode parent in nodes)
            {
                gameCount++;
                //advance the round number if we've completed the round (usually 6 games in a group)
                if (roundNumber == 1 && gameCount > 1 && (gameCount - 1) % groupGames == 0)
                {
                    roundCode = char.ConvertFromUtf32(roundCode.ToCharArray()[0] + 1);
                }
                if (gameCount > 1 && (gameCount - 1) % roundGames == 0)
                {
                    roundNumber++;
                    roundCode = "16";
                }
                if (roundCode == "16" && top16Count == 8)
                {
                    roundCode = "QF";
                }
                else if (roundCode == "QF" && qfCount == 4)
                {
                    roundCode = "SF";
                }
                else if (roundCode == "SF" && sfCount == 2)
                {
                    roundCode = "FF";
                }
                if (roundNumber == 2)
                {
                    if (roundCode == "16")
                    {
                        top16Count++;
                    }
                    else if (roundCode == "QF")
                    {
                        qfCount++;
                    }
                    else if (roundCode == "SF")
                    {
                        sfCount++;
                    }
                }
                HtmlNode dateNode = parent.ChildNodes[1];
                DateTime gameDateTime = DateTime.Parse(dateNode.InnerText.Substring(dateNode.InnerText.IndexOf("(") + 1).Replace(")", " "));

                HtmlNode game = parent.ChildNodes[2];
                string team1Name = game.SelectSingleNode(game.XPath + "/tbody/tr[1]/th[1]/span")?.InnerText?.Replace("&#160;", "");
                int team1Code = GetTeamCode(teams, team1Name);
                string team2Name = game.SelectSingleNode(game.XPath + "/tbody/tr[1]/th[3]/span")?.InnerText?.Replace("&#160;", "");
                int team2Code = GetTeamCode(teams, team2Name);
                string score = game.SelectSingleNode(game.XPath + "/tbody/tr[1]/th[2]/a")?.InnerText;
                string[] scores = score.Split("–");
                int team1NormalTimeScore = int.Parse(scores[0]);
                int team2NormalTimeScore = int.Parse(scores[1]);

                HtmlNode locationNode = parent.ChildNodes[3];
                string location = locationNode.ChildNodes[0].InnerText;

                Game newGame = new()
                {
                    TournamentCode = 315,
                    RoundNumber = roundNumber,
                    RoundCode = roundCode,
                    GameNumber = gameCount,
                    GameTime = gameDateTime,
                    Team1Name = team1Name,
                    Team1Code = team1Code,
                    Team2Name = team2Name,
                    Team2Code = team2Code,
                    Team1NormalTimeScore = team1NormalTimeScore,
                    Team2NormalTimeScore = team2NormalTimeScore,
                    Location = location
                };

                _Games.Add(newGame);

                int gameCode = 0; //await SaveGame(game);

                int goalTime = 0;
                int injuryTime = 0;
                bool IsPenalty = false;
                bool IsOwnGoal = false;

                HtmlNodeCollection team1Goals = game.SelectNodes(game.XPath + "/tbody/tr[2]/td[1]/div/ul/li");
                foreach (HtmlNode item in team1Goals)
                {
                    Goal newGoal = ProcessGoalHTMLNode(item, players, gameCode);
                    _Goals.Add(newGoal);
                }
                HtmlNodeCollection team2Goals = game.SelectNodes(game.XPath + "/tbody/tr[2]/td[3]/div/ul/li");
                foreach (HtmlNode item in team1Goals)
                {
                    Goal newGoal = ProcessGoalHTMLNode(item, players, gameCode);
                    _Goals.Add(newGoal);
                }

                GoalDataAccess goalDA = new(_configuration);
                //await goalDA.SaveItem(goal);

            }

            await LoadGrid(_Games);

            ShowDialog();
            return true;
        }

        private Goal ProcessGoalHTMLNode(HtmlNode item, List<Player> players, int gameCode)
        {
            string playerName = item.SelectSingleNode(item.XPath + "/a").InnerText;
            string goalDetails = item.SelectSingleNode(item.XPath + "/small").InnerText.Replace("&#39;", " ");
            int goalTime = 0;
            int injuryTime = 0;
            bool IsPenalty = false;
            bool IsOwnGoal = false;
            int playerCode = GetPlayerCode(players, playerName);

            Goal goal = new()
            {
                GameCode = gameCode,
                GoalCode = 0,
                PlayerCode = playerCode,
                GoalTime = goalTime,
                InjuryTime = injuryTime,
                IsPenalty = IsPenalty,
                IsOwnGoal = IsOwnGoal
            };

            return goal;
        }

        private int GetPlayerCode(List<Player> players, string playerName)
        {
            int result = 0;
            foreach (Player player in players)
            {
                if (player.PlayerName == playerName)
                {
                    result = player.PlayerCode;
                    break;
                }
            }
            return result;
        }

        private int GetTeamCode(List<Team> teams, string teamName)
        {
            int result = 0;
            foreach (Team team in teams)
            {
                if (team.TeamName == teamName)
                {
                    result = team.TeamCode;
                    break;
                }
            }
            return result;
        }

        private async Task LoadGrid(List<Game> games)
        {
            lstGames.DataContext = games;
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            GameDataAccess da = new(_configuration);
            int count = 0;
            if (_Games.Count > 0)
            {
                List<Game> games = await da.GetListByTournament(_tournamentCode);
                count = games.Count;
            }

            if (count > 0)
            {
                MessageBox.Show("This game already exists in this tournament. Save not successful");
            }
            else
            {

                int i = 0;
                foreach (Game game in _Games)
                {
                    i++;
                    lblStatus.Content = "Saving game " + i.ToString() + "/" + _Games.Count.ToString();
                    await da.SaveMigrationItem(game);
                }
                MessageBox.Show("Saved successfully!");
                Close();
            }
        }

    }

}
