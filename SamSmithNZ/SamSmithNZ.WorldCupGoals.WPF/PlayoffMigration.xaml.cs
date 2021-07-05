using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for Games.xaml
    /// </summary>
    public partial class PlayoffMigration : Window
    {
        private int _tournamentCode;
        private readonly IConfigurationRoot _configuration;
        private List<Playoff> Setups;

        public PlayoffMigration()
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

            //Working for tournament code switch below
            //select t.year, t.tournament_code, f.* from wc_tournament t
            //join wc_tournament_format f on t.format_code = f.format_code
            //join wc_tournament_format_round r on f.round_1_format_code = r.format_round_code and r.is_group_stage = 0

            //TournamentDataAccess da2 = new(_configuration);
            //List<Tournament> tournaments = await da2.GetList(null);
            //foreach (Tournament item in tournaments)
            //{
            //    int roundNumber = 2;
            //    switch (item.TournamentCode)
            //    {
            //        case 10:
            //        case 11:
            //        case 12:
            //            roundNumber = 3;
            //            break;

            //        case 2:
            //        case 3:
            //        case 201:
            //        case 301:
            //        case 302:
            //        case 303:
            //        case 304:
            //        case 305:
            //            roundNumber = 1;
            //            break;
            //    }
            //    GameDataAccess da = new(_configuration);
            //    List<Game> games = await da.GetMigrationPlayoffList(item.TournamentCode, roundNumber);

            //    await LoadGrid(games);

            //    btnSave_Click(null, null);
            //    Debug.WriteLine("Processed tournament " + item.TournamentCode + " (" + item.TournamentYear + ")");
            //}
            //MessageBox.Show("Done! " + tournaments.Count + " tournaments processed!");

            GameDataAccess da = new(_configuration);
            List<Game> games = await da.GetMigrationPlayoffList(_tournamentCode, 2);

            await LoadGrid(games);

            ShowDialog();
            return true;
        }

        private async Task LoadGrid(List<Game> games)
        {
            (int, string) gameNumber1 = (0, "");
            (int, string) gameNumber2 = (0, "");
            Setups = new();
            int i = 0;
            bool thirdPlaceGameExists = false;
            foreach (Game game in games)
            {
                if (game.Team1Code > 0 && game.Team2Code > 0)
                {
                    i++;
                    Playoff setup = new()
                    {
                        TournamentCode = game.TournamentCode,
                        RoundCode = game.RoundCode,
                        GameNumber = game.GameNumber,
                        SortOrder = i
                    };

                    if (game.RoundCode == "FF")
                    {
                        gameNumber1 = GetGameNumber(games, "SF", game.Team1Code);
                        gameNumber2 = GetGameNumber(games, "SF", game.Team2Code);
                        setup.Team1Prereq = "Winner of game " + gameNumber1.Item1.ToString();
                        setup.Team2Prereq = "Winner of game " + gameNumber2.Item1.ToString();
                        Setups.Add(setup);
                    }
                    else if (game.RoundCode == "3P")
                    {
                        thirdPlaceGameExists = true;
                        gameNumber1 = GetGameNumber(games, "SF", game.Team1Code);
                        gameNumber2 = GetGameNumber(games, "SF", game.Team2Code);
                        setup.Team1Prereq = "Loser of game " + gameNumber1.Item1.ToString();
                        setup.Team2Prereq = "Loser of game " + gameNumber2.Item1.ToString();
                        Setups.Add(setup);
                    }
                    else if (game.RoundCode == "SF")
                    {
                        gameNumber1 = GetGameNumber(games, "QF", game.Team1Code);
                        gameNumber2 = GetGameNumber(games, "QF", game.Team2Code);
                        setup.Team1Prereq = "Winner of game " + gameNumber1.Item1.ToString();
                        setup.Team2Prereq = "Winner of game " + gameNumber2.Item1.ToString();
                        Setups.Add(setup);
                    }
                    else if (game.RoundCode == "QF")
                    {
                        gameNumber1 = GetGameNumber(games, "16", game.Team1Code);
                        gameNumber2 = GetGameNumber(games, "16", game.Team2Code);
                        setup.Team1Prereq = "Winner of game " + gameNumber1.Item1.ToString();
                        setup.Team2Prereq = "Winner of game " + gameNumber2.Item1.ToString();
                        Setups.Add(setup);
                    }
                    else if (game.RoundCode == "16")
                    {
                        (string, int) gameGroupNumber1 = await GetGroupDetails(game.TournamentCode, game.RoundNumber, games, "16", game.Team1Code);
                        (string, int) gameGroupNumber2 = await GetGroupDetails(game.TournamentCode, game.RoundNumber, games, "16", game.Team2Code);
                        setup.Team1Prereq = "Group " + gameGroupNumber1.Item1.ToString() + " " + ConvertNumberToRank(gameGroupNumber1.Item2) + " place";
                        setup.Team2Prereq = "Group " + gameGroupNumber2.Item1.ToString() + " " + ConvertNumberToRank(gameGroupNumber2.Item2) + " place";
                        Setups.Add(setup);
                    }
                }
            }

            //Arrange the sort order
            if (Setups.Count > 0)
            {
                int adjustment = 0;
                int totalPlayoffGames = Setups.Count;
                if (thirdPlaceGameExists)
                {
                    adjustment = 1;
                }
                SetSortOrder("3P", 0, totalPlayoffGames);
                SetSortOrder("FF", 0, totalPlayoffGames - adjustment);
                adjustment++; //add final game to adjustment

                if (Setups.Count > 2)
                {
                    Playoff finalGame = FindGame("FF", 0);
                    //Find semi-final game for final team 2 game
                    Playoff sf2 = FindGame("SF", GetGameNumber(finalGame.Team2Prereq));
                    //Find semi-final game for final team 1 game
                    Playoff sf1 = FindGame("SF", GetGameNumber(finalGame.Team1Prereq));
                    SetSortOrder("SF", sf2.GameNumber, totalPlayoffGames - adjustment);
                    SetSortOrder("SF", sf1.GameNumber, totalPlayoffGames - adjustment - 1);
                    adjustment += 2; //add semi-final games to adjustment

                    if (Setups.Count > 4)
                    {
                        //Find the quarter-final games for the semi-final games
                        //Find quarter-final game for semi final game 2, team 2 
                        Playoff qf4 = FindGame("QF", GetGameNumber(sf2.Team2Prereq));
                        //Find quarter-final game for semi final game 2, team 1 
                        Playoff qf3 = FindGame("QF", GetGameNumber(sf2.Team1Prereq));
                        //Find quarter-final game for semi final game 1, team 2 
                        Playoff qf2 = FindGame("QF", GetGameNumber(sf1.Team2Prereq));
                        //Find quarter-final game for semi final game 1, team 1 
                        Playoff qf1 = FindGame("QF", GetGameNumber(sf1.Team1Prereq));
                        SetSortOrder("QF", qf4.GameNumber, totalPlayoffGames - adjustment);
                        SetSortOrder("QF", qf3.GameNumber, totalPlayoffGames - adjustment - 1);
                        SetSortOrder("QF", qf2.GameNumber, totalPlayoffGames - adjustment - 2);
                        SetSortOrder("QF", qf1.GameNumber, totalPlayoffGames - adjustment - 3);

                        if (Setups.Count > 8)
                        {
                            //Find the top 16 games for quarter-final games
                            //Find quarter-final game for quarter-final game 4, team 2 
                            Playoff top168 = FindGame("16", GetGameNumber(qf4.Team2Prereq));
                            //Find quarter-final game for quarter-final game 4, team 1 
                            Playoff top167 = FindGame("16", GetGameNumber(qf4.Team1Prereq));
                            //Find quarter-final game for quarter-final game 3, team 2 
                            Playoff top166 = FindGame("16", GetGameNumber(qf3.Team2Prereq));
                            //Find quarter-final game for quarter-final game 3, team 1 
                            Playoff top165 = FindGame("16", GetGameNumber(qf3.Team1Prereq));
                            //Find quarter-final game for quarter-final game 2, team 2 
                            Playoff top164 = FindGame("16", GetGameNumber(qf2.Team2Prereq));
                            //Find quarter-final game for quarter-final game 2, team 1 
                            Playoff top163 = FindGame("16", GetGameNumber(qf2.Team1Prereq));
                            //Find quarter-final game for quarter-final game 1, team 2 
                            Playoff top162 = FindGame("16", GetGameNumber(qf1.Team2Prereq));
                            //Find quarter-final game for quarter-final game 1, team 1 
                            Playoff top161 = FindGame("16", GetGameNumber(qf1.Team1Prereq));
                            SetSortOrder("16", top168.GameNumber, totalPlayoffGames - adjustment);
                            SetSortOrder("16", top167.GameNumber, totalPlayoffGames - adjustment - 1);
                            SetSortOrder("16", top166.GameNumber, totalPlayoffGames - adjustment - 2);
                            SetSortOrder("16", top165.GameNumber, totalPlayoffGames - adjustment - 3);
                            SetSortOrder("16", top164.GameNumber, totalPlayoffGames - adjustment - 4);
                            SetSortOrder("16", top163.GameNumber, totalPlayoffGames - adjustment - 5);
                            SetSortOrder("16", top162.GameNumber, totalPlayoffGames - adjustment - 6);
                            SetSortOrder("16", top161.GameNumber, totalPlayoffGames - adjustment - 7);
                        }
                    }
                }
            }

            lstGames.DataContext = Setups;
        }

        private void SetSortOrder(string roundCode, int gameNumber, int sortOrder)
        {
            foreach (Playoff item in Setups)
            {
                if (item.RoundCode == roundCode && (item.GameNumber == gameNumber || gameNumber == 0))
                {
                    item.SortOrder = sortOrder;
                }
            }
        }

        private string ConvertNumberToRank(int number)
        {
            if (number == 1)
            {
                return "1st";
            }
            else if (number == 2)
            {
                return "2nd";
            }
            else if (number == 3)
            {
                return "3rd";
            }
            else if (number == 4)
            {
                return "4th";
            }
            else
            {
                return number.ToString();
            }
        }

        private int GetGameNumber(string prereq)
        {
            string result = prereq.Replace("Winner of game ", "");
            int gameNumber;
            int.TryParse(result, out gameNumber);
            return gameNumber;
        }

        private Playoff FindGame(string roundCode, int gameNumber)
        {
            Playoff game = null;
            foreach (Playoff item in Setups)
            {
                if (item.RoundCode == roundCode && (item.GameNumber == gameNumber || gameNumber == 0))
                {
                    game = item;
                }
            }
            return game;
        }

        private (int, string) GetGameNumber(List<Game> games, string roundCode, int teamCode)
        {
            int gameNumberResult = 0;
            string roundCodeResult = "";
            foreach (Game game in games)
            {
                if (game.RoundCode == roundCode || roundCode == null)
                {
                    if (game.Team1Code == teamCode)
                    {
                        gameNumberResult = game.GameNumber;
                        roundCodeResult = game.RoundCode;
                        break;
                    }
                    if (game.Team2Code == teamCode)
                    {
                        gameNumberResult = game.GameNumber;
                        roundCodeResult = game.RoundCode;
                        break;
                    }
                }
            }
            return new(gameNumberResult, roundCodeResult);
        }

        private async Task<(string, int)> GetGroupDetails(int tournamentCode, int roundNumber, List<Game> games, string roundCode, int teamCode)
        {
            (string, int) groupDetail = ("", 0);
            foreach (Game game in games)
            {
                if (game.RoundCode == roundCode || roundCode == null)
                {
                    if (game.Team1Code == teamCode)
                    {
                        groupDetail = await GetGroupTeamRanking(tournamentCode, roundNumber - 1, teamCode);
                        break;
                    }
                    if (game.Team2Code == teamCode)
                    {
                        groupDetail = await GetGroupTeamRanking(tournamentCode, roundNumber - 1, teamCode);
                        break;
                    }
                }
            }
            return (groupDetail.Item1, groupDetail.Item2);
        }

        private async Task<(string, int)> GetGroupTeamRanking(int tournamentCode, int roundNumber, int teamCode)
        {
            int gameRanking = 0;
            string roundCode = "";
            GroupDataAccess da = new(_configuration);
            List<Group> groupTeams = await da.GetList(tournamentCode, roundNumber, null);
            foreach (Group groupTeam in groupTeams)
            {
                if (groupTeam.TeamCode == teamCode && groupTeam.BaseRoundCode != "3rd")
                {
                    gameRanking = groupTeam.GroupRanking;
                    roundCode = groupTeam.RoundCode;
                    break;
                }
            }
            return (roundCode, gameRanking);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            PlayoffDataAccess da = new(_configuration);
            foreach (Playoff setup in Setups)
            {
                await da.SaveItem(setup);
            }
            MessageBox.Show("Saved successfully!");
            Close();
        }

    }

}
