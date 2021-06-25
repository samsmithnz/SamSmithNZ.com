using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for Games.xaml
    /// </summary>
    public partial class PlayoffMigration : Window
    {
        private int _tournamentCode;
        private readonly IConfigurationRoot _configuration;
        private List<PlayoffSetup> Setups;

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
            foreach (Game game in games)
            {
                if (game.Team1Code > 0 && game.Team2Code > 0)
                {
                    PlayoffSetup setup = new()
                    {
                        TournamentCode = game.TournamentCode,
                        RoundNumber = game.RoundNumber,
                        RoundCode = game.RoundCode,
                        GameNumber = game.GameNumber
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
                        setup.Team1Prereq = "Group " + gameGroupNumber1.Item1.ToString() + " " + gameGroupNumber1.Item2.ToString() + " place finisher";
                        setup.Team2Prereq = "Group " + gameGroupNumber2.Item1.ToString() + " " + gameGroupNumber2.Item2.ToString() + " place finisher";
                        Setups.Add(setup);
                    }
                }

            }

            lstGames.DataContext = Setups;
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
            PlayoffSetupDataAccess da = new(_configuration);
            foreach (PlayoffSetup setup in Setups)
            {
                await da.SaveItem(setup);
            }
            MessageBox.Show("Saved successfully!");
            Close();
        }

    }

}
