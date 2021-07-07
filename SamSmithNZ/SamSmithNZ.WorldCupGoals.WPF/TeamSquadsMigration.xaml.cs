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
    public partial class TeamSquadsMigration : Window
    {
        private int _tournamentCode;
        private readonly IConfigurationRoot _configuration;
        private List<Player> _Players;

        public TeamSquadsMigration()
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

            string url = "https://en.wikipedia.org/wiki/UEFA_Euro_2000_squads";
            lblURL.Content = url;
            HtmlWeb web = new();
            HtmlDocument doc = web.Load(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(@"//*[@class=""mw-headline""]");

            //span class="mw-headline": France
            //tr class="nat-fs-player": player

            _Players = new();
            string teamName = "";
            foreach (HtmlNode parent in nodes)
            {
                //Validate the team name
                teamName = parent.InnerText;
                int teamCode = GetTeamCode(teams, teamName);

                if (teamCode > 0)
                {
                    string currentXPath = parent.XPath;
                    HtmlNode nextNode = parent.ParentNode.NextSibling;//.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling.NextSibling;
                    do
                    {
                        nextNode = nextNode.NextSibling;
                    } while (nextNode != null && nextNode.Name != "table");

                    if (nextNode != null && nextNode.Name == "table")
                    {
                        //Get the players from the table
                        HtmlNodeCollection playerNodes = doc.DocumentNode.SelectNodes(nextNode.XPath + "/tbody/tr");
                        for (int i = 1; i < playerNodes.Count; i++) //Skip row 0, it's the header
                        {
                            HtmlNode playerRow = playerNodes[i];
                            string number = playerRow.SelectSingleNode(playerRow.XPath + "/td[1]")?.InnerText?.Replace("\n", "");
                            string position = playerRow.SelectSingleNode(playerRow.XPath + "/td[2]/a")?.InnerText?.Replace("\n", "");
                            //if (teamCode == 68)
                            //{
                            //    int j = 0;
                            //}
                            string playerName = playerRow.SelectSingleNode(playerRow.XPath + "/th/span")?.InnerText?.Replace("\n", "");
                            if (playerName == null)
                            {
                                playerName = playerRow.SelectSingleNode(playerRow.XPath + "/th")?.InnerText?.Replace("\n", "");
                            }
                            string playerCaptainString = playerRow.SelectSingleNode(playerRow.XPath + "/th/small")?.InnerText;
                            bool isCaptain = false;
                            if (playerCaptainString?.IndexOf("captain") >= 0)
                            {
                                isCaptain = true;
                            }
                            string dateOfBirthEntireString = playerRow.SelectSingleNode(playerRow.XPath + "/td[3]")?.InnerText?.Replace("\n", "");
                            string dateOfBirth = (dateOfBirthEntireString.Trim().Substring(0, dateOfBirthEntireString.IndexOf(")"))).Replace("(", "").Replace(")", "");
                            string club = playerRow.SelectSingleNode(playerRow.XPath + "/td[6]/a")?.InnerText?.Replace("\n", "");

                            Player newPlayer = new()
                            {
                                TournamentCode = _tournamentCode,
                                TeamCode = teamCode,
                                TeamName = teamName,
                                Number = int.Parse(number),
                                Position = position,
                                IsCaptain = isCaptain,
                                PlayerName = playerName,
                                DateOfBirth = DateTime.Parse(dateOfBirth),
                                ClubName = club
                            };

                            _Players.Add(newPlayer);
                        }
                    }
                }
            }

            await LoadGrid(_Players);

            ShowDialog();
            return true;
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

        private async Task LoadGrid(List<Player> players)
        {
            lstGames.DataContext = players;
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            PlayerDataAccess da = new(_configuration);
            List<Player> players = await da.GetPlayersByTournament(_tournamentCode);
            int count = players.Count;

            if (count > 0)
            {
                MessageBox.Show("This player already exists in this tournament. Save not successful");
            }
            else
            {
                int i = 0;
                foreach (Player player in _Players)
                {
                    i++;
                    lblStatus.Content = "Saving player " + i.ToString() + "/" + _Players.Count.ToString();
                    await da.SaveItem(player);
                }
                MessageBox.Show("Saved successfully!");
                Close();
            }
        }

    }

}
