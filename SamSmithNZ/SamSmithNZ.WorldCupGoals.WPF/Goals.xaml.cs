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
    /// Interaction logic for Goals.xaml
    /// </summary>
    public partial class Goals : Window
    {
        private int _tournamentCode;
        private int _gameCode;
        private int _iGoalsToAssign;
        private readonly IConfigurationRoot _configuration;

        public Goals()
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

            await LoadGoals(gameCode);

            ShowDialog();
            return true;
        }

        private async Task LoadGoals(int gameCode)
        {
            GoalDataAccess da = new(_configuration);
            List<Goal> goals = await da.GetListByGame(gameCode);
            lstGoals.DataContext = goals;

            if (_iGoalsToAssign == 0)
            {
                btnAddGoal.IsEnabled = false;
            }

            lblGoalsToAssign.Content = _iGoalsToAssign.ToString();
        }

        private async void btnGoalEdit_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
            {
                return;
            }

            //dsWorldCup.GameListForGoalAssigningRow dr = (dsWorldCup.GameListForGoalAssigningRow)lstGames.ItemContainerGenerator.ItemFromContainer(dep);
            //System.Data.DataRowView dr = (System.Data.DataRowView)lstGoals.ItemContainerGenerator.ItemFromContainer(dep);
            Goal goal = (Goal)lstGoals.ItemContainerGenerator.ItemFromContainer(dep);

            AssignGoals AssignGoalsi = new();
            await AssignGoalsi.ShowForm(_tournamentCode, _gameCode, goal.GoalCode);
            await LoadGoals(_gameCode);
        }

        private async void AddGoal_Click(object sender, RoutedEventArgs e)
        {
            AssignGoals AssignGoalsi = new();
            if (await AssignGoalsi.ShowForm(_tournamentCode, _gameCode, 0) == true)
            {
                _iGoalsToAssign--;
            }
            await LoadGoals(_gameCode);
            if (_iGoalsToAssign == 0)
            {
                Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
