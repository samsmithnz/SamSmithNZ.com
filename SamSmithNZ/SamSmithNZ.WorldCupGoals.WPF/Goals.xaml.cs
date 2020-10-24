using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
               .AddJsonFile("appsettings.json");
            _configuration = config.Build();
        }

        public async Task<bool> ShowForm(int tournamentCode, int gameCode, int iGoalsToAssign)
        {
            _tournamentCode = tournamentCode;
            _gameCode = gameCode;
            _iGoalsToAssign = iGoalsToAssign;

            GameDataAccess da = new GameDataAccess(_configuration);
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
            GoalDataAccess da = new GoalDataAccess(_configuration);
            List<Goal> goals = await da.GetList(gameCode);
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

            AssignGoals AssignGoalsi = new AssignGoals();
            await AssignGoalsi.ShowForm(_tournamentCode, _gameCode, goal.GoalCode);
            await LoadGoals(_gameCode);
        }

        private async void AddGoal_Click(object sender, RoutedEventArgs e)
        {
            AssignGoals AssignGoalsi = new AssignGoals();
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
