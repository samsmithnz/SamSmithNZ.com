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
    /// Interaction logic for AssignGoals.xaml
    /// </summary>
    public partial class AssignGoals : Window
    {
        private Boolean _bResult;
        private int _gameCode;
        private int _goalCode;

        public AssignGoals()
        {
            InitializeComponent();
        }

        public async Task<Boolean> ShowForm(int iTournamentCode, int iGameCode, int iGoalCode)
        {
            _gameCode = iGameCode;
            _goalCode = iGoalCode;

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
            if (iGoalCode == 0)
            {

            }
            else //it's an existing goal, load and populate the form
            {
                GoalDataAccess da3 = new GoalDataAccess();
                List<Goal> goals = await da3.GetListAsync(iGameCode);
                Goal goal = null;
                foreach (Goal item in goals)
                {
                    if (item.GoalCode == iGoalCode)
                    {
                        goal = item;
                        break;
                    }
                }
                cboPlayer.SelectedValue = goal.PlayerCode;
                //chkShowExtraTime.IsChecked = (drGoal.injury_time == 0);
                sliNormalTime.Value = goal.GoalTime;
                sliInjuryTime.Value = goal.InjuryTime;
                chkIsPenalty.IsChecked = goal.IsPenalty;
                chkIsOwnGoal.IsChecked = goal.IsOwnGoal;
            }

            cboPlayer.Focus();

            //dsWorldCup.GameListForGoalAssigningDataTable dtGame = clsWCDataAccess.GameListForGoalAssigning(iTournamentCode);
            //lstGames.DataContext = dtGame.DefaultView;

            this.ShowDialog();
            return _bResult;
        }

        private void sliNormalTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtNormalTime.Text = sliNormalTime.Value.ToString();
            UpdateGoalAppearance(Convert.ToInt16(cboPlayer.SelectedValue), Convert.ToInt32(sliNormalTime.Value), Convert.ToInt32(sliInjuryTime.Value), chkIsPenalty.IsChecked, chkIsOwnGoal.IsChecked);
        }

        private void sliInjuryTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtInjuryTime.Text = sliInjuryTime.Value.ToString();
            UpdateGoalAppearance(Convert.ToInt16(cboPlayer.SelectedValue), Convert.ToInt32(sliNormalTime.Value), Convert.ToInt32(sliInjuryTime.Value), chkIsPenalty.IsChecked, chkIsOwnGoal.IsChecked);
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

            lblGoalAppearance.Content = sGoal;
        }

        private void chkIsPenalty_Checked(object sender, RoutedEventArgs e)
        {
            UpdateGoalAppearance(Convert.ToInt16(cboPlayer.SelectedValue), Convert.ToInt32(sliNormalTime.Value), Convert.ToInt32(sliInjuryTime.Value), chkIsPenalty.IsChecked, chkIsOwnGoal.IsChecked);
        }

        private void chkIsOwnGoal_Checked(object sender, RoutedEventArgs e)
        {
            UpdateGoalAppearance(Convert.ToInt16(cboPlayer.SelectedValue), Convert.ToInt32(sliNormalTime.Value), Convert.ToInt32(sliInjuryTime.Value), chkIsPenalty.IsChecked, chkIsOwnGoal.IsChecked);
        }

        private void chkShowExtraTime_Checked(object sender, RoutedEventArgs e)
        {
            sliNormalTime.Maximum = 120;
            sliNormalTime.Width = 120 * 4;
        }

        private void chkShowExtraTime_Unchecked(object sender, RoutedEventArgs e)
        {
            sliNormalTime.Maximum = 90;
            sliNormalTime.Width = 90 * 4;
        }

        private void txtNormalTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            int normalTime = 0;
            if (int.TryParse(txtNormalTime.Text, out normalTime) == true)
            {
                sliNormalTime.Value = normalTime;
            }
        }

        private void txtInjuryTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            int injuryTime = 0;
            if (int.TryParse(txtInjuryTime.Text, out injuryTime) == true)
            {
                sliInjuryTime.Value = injuryTime;
            }
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSave() == true)
            {
                bool bIsPenalty = Convert.ToBoolean(chkIsPenalty.IsChecked);
                bool bIsOwnGoal = Convert.ToBoolean(chkIsOwnGoal.IsChecked);

                Goal goal = new Goal();
                goal.GoalCode = _goalCode;
                goal.GameCode = _gameCode;
                goal.PlayerCode = Convert.ToInt16(cboPlayer.SelectedValue);
                goal.GoalTime = Convert.ToInt16(sliNormalTime.Value);
                goal.InjuryTime = Convert.ToInt16(sliInjuryTime.Value);
                goal.IsPenalty = bIsPenalty;
                goal.IsOwnGoal = bIsOwnGoal;

                GoalDataAccess da = new GoalDataAccess();
                await da.SaveItemAsync(goal);

                _bResult = true;
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

            if (Convert.ToInt16(sliNormalTime.Value) == 0)
            {
                MessageBox.Show("Select a goal time");
                return false;
            }

            if (chkIsPenalty.IsChecked == null || chkIsOwnGoal.IsChecked == null)
            {
                MessageBox.Show("Penalty and Own Goal checkboxes cannot be null");
                return false;
            }

            return true;
        }

    }
}
