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
        private List<Game> Games;

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

            string url = "https://en.wikipedia.org/wiki/UEFA_Euro_2016#Group_A";
            HtmlWeb web = new();
            HtmlDocument doc = web.Load(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(@"//*[@class=""footballbox""]");

            foreach (HtmlNode parent in nodes)
            {
                HtmlNode dateNode = parent.ChildNodes[1];
                DateTime gameDateTime = DateTime.Parse(dateNode.InnerText.Substring(dateNode.InnerText.IndexOf("(") + 1).Replace(")", " "));

                HtmlNode game = parent.ChildNodes[2];
                string team1Name = game.SelectSingleNode(game.XPath + "/tbody/tr[1]/th[1]/span")?.InnerText?.Replace("&#160;", "");
                string team2Name = game.SelectSingleNode(game.XPath + "/tbody/tr[1]/th[3]/span")?.InnerText?.Replace("&#160;", "");
                string score = game.SelectSingleNode(game.XPath + "/tbody/tr[1]/th[2]/a")?.InnerText;

                HtmlNodeCollection team1Goals = game.SelectNodes(game.XPath + "/tbody/tr[2]/td[1]/div/ul/li");
                HtmlNodeCollection team2Goals = game.SelectNodes(game.XPath + "/tbody/tr[2]/td[3]/div/ul/li");

                HtmlNode locationNode = parent.ChildNodes[3];
                string location = locationNode.ChildNodes[0].InnerText;
            }

            //GameDataAccess da = new(_configuration);
            //List<Game> games = await da.GetMigrationPlayoffList(_tournamentCode, 2);

            //await LoadGrid(games);

            ShowDialog();
            return true;
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
            //PlayoffDataAccess da = new(_configuration);
            //foreach (Playoff setup in Setups)
            //{
            //    await da.SaveItem(setup);
            //}
            //MessageBox.Show("Saved successfully!");
            Close();
        }

    }

}
