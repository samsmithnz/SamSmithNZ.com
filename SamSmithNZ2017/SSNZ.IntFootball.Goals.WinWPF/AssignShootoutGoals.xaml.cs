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
    /// Interaction logic for AssignShootoutGoals.xaml
    /// </summary>
    public partial class AssignShootoutGoals : Window
    {
        private Boolean _bResult;
        private short _gameCode;
        private short _penaltyCode;
        private short _order;

        public AssignShootoutGoals()
        {
            InitializeComponent();
        }

        public async Task<Boolean> ShowForm(short iTournamentCode, short iGameCode, short penaltyCode, short iOrder)
        {
            _gameCode = iGameCode;
            _penaltyCode = penaltyCode;
            _order = iOrder;

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
            PlayerDataAccess da2 = new PlayerDataAccess();
            List<Player> players = await da2.GetListAsync(iGameCode);

            lblGame.Content = "#" + game.GameNumber + ": " + game.GameTime.ToString("dd-MMM-yyyy hh:mm:sstt") + "   " + game.Team1Name + " vs " + game.Team2Name + ": " + Utility.GetGameScore(game);

            cboPlayer.DataContext = players;
            cboPlayer.DisplayMemberPath = "PlayerName";
            cboPlayer.SelectedValuePath = "PlayerCode";

            //We are adding a new goal to the game
            if (penaltyCode == 0)
            {

            }
            else //it's an existing goal, load and populate the form
            {
                PenaltyShootoutGoalDataAccess da3 = new PenaltyShootoutGoalDataAccess();
                List<PenaltyShootoutGoal> goals = await da3.GetListAsync(iGameCode);
                PenaltyShootoutGoal goal = null;
                foreach (PenaltyShootoutGoal item in goals)
                {
                    if (item.PenaltyCode == penaltyCode)
                    {
                        goal = item;
                        break;
                    }
                }
                cboPlayer.SelectedValue = goal.PlayerCode;
                chkScored.IsChecked = goal.Scored;
            }

            //dsWorldCup.GameListForGoalAssigningDataTable dtGame = clsWCDataAccess.GameListForGoalAssigning(iTournamentCode);
            //lstGames.DataContext = dtGame.DefaultView;

            cboPlayer.Focus();

            this.ShowDialog();
            return _bResult;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _bResult = false;
            this.Close();
        }

        private void UpdateGoalAppearance(short iPlayerCode, int iGoalTime, int iExtraTime, Boolean? bIsPenalty, Boolean? bIsOwnGoal)
        {
            String sGoal = "";

            if (iPlayerCode > 0 && iGoalTime > 0)
            {
                //sGoal = cboPlayer.SelectedItem.player_name;

                // sGoal = sGoal + " - " + iGoalTime.ToString();
                sGoal = iGoalTime.ToString();

                if (iExtraTime > 0)
                {
                    sGoal = sGoal + "+" + iExtraTime.ToString() + "'";
                }
                else
                {
                    sGoal = sGoal + "'";
                }
                if (bIsPenalty == true)
                {
                    sGoal = sGoal + " (pen)";
                }
                if (bIsOwnGoal == true)
                {
                    sGoal = sGoal + " (og)";
                }
            }

        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSave() == true)
            {
                bool bScored = Convert.ToBoolean(chkScored.IsChecked);

                PenaltyShootoutGoal goal = new PenaltyShootoutGoal();
                goal.PenaltyCode = _penaltyCode;
                goal.GameCode = _gameCode;
                goal.PlayerCode = Convert.ToInt16(cboPlayer.SelectedValue);
                goal.PenaltyOrder = _order;
                goal.Scored = bScored;

                PenaltyShootoutGoalDataAccess da = new PenaltyShootoutGoalDataAccess();
                await da.SaveItemAsync(goal);

                _bResult = bScored;
                this.Close();
            }
        }

        private Boolean ValidateSave()
        {
            if (Convert.ToInt16(cboPlayer.SelectedValue) == 0)
            {
                MessageBox.Show("Select player");
                return false;
            }

            if (chkScored.IsChecked == null)
            {
                MessageBox.Show("The Scored checkbox cannot be null");
                return false;
            }

            return true;
        }

    }
}
