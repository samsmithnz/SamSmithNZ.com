using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for ShootoutGoals.xaml
    /// </summary>
    public partial class ShootoutGoals : Window
    {
        private int _tournamentCode;
        private int _gameCode;
        private int _iGoalsToAssign;
        private int _iMaxOrder;
        private readonly IConfigurationRoot _configuration;

        public ShootoutGoals()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Main>(true);
            _configuration = config.Build();
        }

        public async Task<bool> ShowForm(int tournamentCode, int gameCode, int iGoalsToAssign)
        {
            _tournamentCode = tournamentCode;
            _gameCode = gameCode;
            _iGoalsToAssign = iGoalsToAssign;

            GameDataAccess da = new(_configuration);
            List<Game> games = await da.GetListByTournament(tournamentCode);
            Game game = null;
            foreach (Game item in games)
            {
                if (item.GameCode == gameCode)
                {
                    game = item;
                    break;
                }
            }
            lblGame.Content = "#" + game.GameNumber + ": " + game.GameTime.ToString("dd-MMM-yyyy hh:mm:sstt") + "   " + game.Team1Name + " vs " + game.Team2Name + ": " + Utility.GetGameScore(game);

            await LoadShootoutGoals(gameCode);

            ShowDialog();
            return true;
        }

        private async Task LoadShootoutGoals(int gameCode)
        {
            PenaltyShootoutGoalDataAccess da = new(_configuration);
            List<PenaltyShootoutGoal> goals = await da.GetList(gameCode);
            lstGoals.DataContext = goals;

            if (_iGoalsToAssign == 0)
            {
                btnAddGoal.IsEnabled = false;
            }
            _iMaxOrder = Convert.ToInt32(goals.Count);

            lblGoalsToAssign.Content = _iGoalsToAssign.ToString();
        }

        private async void btnGoalEdit_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && (dep is not ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
            {
                return;
            }

            //dsWorldCup.GameListForGoalAssigningRow dr = (dsWorldCup.GameListForGoalAssigningRow)lstGames.ItemContainerGenerator.ItemFromContainer(dep);
            PenaltyShootoutGoal dr = (PenaltyShootoutGoal)lstGoals.ItemContainerGenerator.ItemFromContainer(dep);

            AssignShootoutGoals AssignShootoutGoalsi = new();
            await AssignShootoutGoalsi.ShowForm(_tournamentCode, _gameCode, Convert.ToInt32(dr.PenaltyCode), Convert.ToInt32(dr.PenaltyOrder));

            await LoadShootoutGoals(_gameCode);
        }

        private async void AddGoal_Click(object sender, RoutedEventArgs e)
        {
            AssignShootoutGoals AssignShootoutGoalsi = new();
            if (await AssignShootoutGoalsi.ShowForm(_tournamentCode, _gameCode, 0, Convert.ToInt32(_iMaxOrder + 1)) == true)
            {
                _iGoalsToAssign--;
            }
            await LoadShootoutGoals(_gameCode);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
