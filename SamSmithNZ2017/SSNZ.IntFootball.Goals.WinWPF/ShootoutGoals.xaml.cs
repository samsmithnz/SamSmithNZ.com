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
using SSNZ.IntFootball.Goals.WinWPF;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Goals.WinWPF
{
    /// <summary>
    /// Interaction logic for ShootoutGoals.xaml
    /// </summary>
    public partial class ShootoutGoals : Window
    {
        private short _iTournamentCode;
        private short _iGameCode;
        private short _iGoalsToAssign;
        private short _iMaxOrder;

        public ShootoutGoals()
        {
            InitializeComponent();
        }

        public async Task<Boolean> ShowForm(short iTournamentCode, short iGameCode, short iGoalsToAssign)
        {
            _iTournamentCode = iTournamentCode;
            _iGameCode = iGameCode;
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

            await LoadShootoutGoals(iGameCode);

            this.ShowDialog();
            return true;
        }

        private async Task LoadShootoutGoals(short gameCode)
        {
            PenaltyShootoutGoalDataAccess da = new PenaltyShootoutGoalDataAccess();
            List<PenaltyShootoutGoal> goals = await da.GetListAsync(gameCode);
            lstGoals.DataContext = goals;

            if (_iGoalsToAssign == 0)
            {
                btnAddGoal.IsEnabled = false;
            }
            _iMaxOrder = Convert.ToInt16(goals.Count);

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
            PenaltyShootoutGoal dr = (PenaltyShootoutGoal)lstGoals.ItemContainerGenerator.ItemFromContainer(dep);

            AssignShootoutGoals AssignShootoutGoalsi = new AssignShootoutGoals();
            await AssignShootoutGoalsi.ShowForm(_iTournamentCode, _iGameCode, Convert.ToInt16(dr.PenaltyCode), Convert.ToInt16(dr.PenaltyOrder));

            await LoadShootoutGoals(_iGameCode);
        }

        private async void btnAddGoal_Click(object sender, RoutedEventArgs e)
        {
            AssignShootoutGoals AssignShootoutGoalsi = new AssignShootoutGoals();
            if (await AssignShootoutGoalsi.ShowForm(_iTournamentCode, _iGameCode, 0, Convert.ToInt16(_iMaxOrder + 1)) == true)
            {
                _iGoalsToAssign--;
            }
            await LoadShootoutGoals(_iGameCode);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
