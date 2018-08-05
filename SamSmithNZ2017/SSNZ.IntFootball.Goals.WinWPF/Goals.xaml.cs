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
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Threading.Tasks;
using SSNZ.IntFootball.Goals.WinWPF;

namespace SSNZ.IntFootball.Goals.WinWPF
{
    /// <summary>
    /// Interaction logic for Goals.xaml
    /// </summary>
    public partial class Goals : Window
    {
        private short _tournamentCode;
        private short _gameCode;
        private short _iGoalsToAssign;

        public Goals()
        {
            InitializeComponent();
        }

        public async Task<Boolean> ShowForm(short iTournamentCode, short iGameCode, short iGoalsToAssign)
        {
            _tournamentCode = iTournamentCode;
            _gameCode = iGameCode;
            _iGoalsToAssign = iGoalsToAssign;

            GameDataAccess da = new GameDataAccess();
            List<Game> games = await da.GetListAsyncByTournament(iTournamentCode);
            Game game = null;
            foreach (Game item in games)
            {
                if (item.GameCode == iGameCode)
                {
                    game = item;
                    break;
                }
            }
            lblGame.Content = "#" + game.GameNumber + ": " + game.GameTime.ToString("dd-MMM-yyyy hh:mm:sstt") + "   " + game.Team1Name + " vs " + game.Team2Name + ": " + Utility.GetGameScore(game);

            await LoadGoals(iGameCode);

            this.ShowDialog();
            return true;
        }

        private async Task LoadGoals(short gameCode)
        {
            GoalDataAccess da = new GoalDataAccess();
            List<Goal> goals = await da.GetListAsync(gameCode);
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

        private async void btnAddGoal_Click(object sender, RoutedEventArgs e)
        {
            AssignGoals AssignGoalsi = new AssignGoals();
            if (await AssignGoalsi.ShowForm(_tournamentCode, _gameCode, 0) == true)
            {
                _iGoalsToAssign--;
            }
            await LoadGoals(_gameCode);
            if (_iGoalsToAssign == 0)
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
