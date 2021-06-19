using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for AssignGoals.xaml
    /// </summary>
    public partial class AssignGoals : Window
    {
        private bool _result;
        private int _gameCode;
        private int _goalCode;
        private readonly IConfigurationRoot _configuration;

        public AssignGoals()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Main>(true);
            _configuration = config.Build();
        }

        public async Task<bool> ShowForm(int tournamentCode, int gameCode, int goalCode)
        {
            _gameCode = gameCode;
            _goalCode = goalCode;

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
            PlayerDataAccess da2 = new(_configuration);
            List<Player> players = await da2.GetList(gameCode);

            lblGame.Content = "#" + game.GameNumber + ": " + game.GameTime.ToString("dd-MMM-yyyy hh:mm:sstt") + "   " + game.Team1Name + " vs " + game.Team2Name + ": " + Utility.GetGameScore(game);

            cboPlayer.DataContext = players;
            cboPlayer.DisplayMemberPath = "PlayerName";
            cboPlayer.SelectedValuePath = "PlayerCode";

            //We are adding a new goal to the game
            if (goalCode == 0)
            {
                //do nothing?
            }
            else //it's an existing goal, load and populate the form
            {
                GoalDataAccess da3 = new(_configuration);
                List<Goal> goals = await da3.GetListByGame(gameCode);
                Goal goal = null;
                foreach (Goal item in goals)
                {
                    if (item.GoalCode == goalCode)
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

            //dsWorldCup.GameListForGoalAssigningDataTable dtGame = clsWCDataAccess.GameListForGoalAssigning(tournamentCode);
            //lstGames.DataContext = dtGame.DefaultView;

            ShowDialog();
            return _result;
        }

        private void sliNormalTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtNormalTime.Text = sliNormalTime.Value.ToString();
            UpdateGoalAppearance(Convert.ToInt32(cboPlayer.SelectedValue), Convert.ToInt32(sliNormalTime.Value), Convert.ToInt32(sliInjuryTime.Value), chkIsPenalty.IsChecked, chkIsOwnGoal.IsChecked);
        }

        private void sliInjuryTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtInjuryTime.Text = sliInjuryTime.Value.ToString();
            UpdateGoalAppearance(Convert.ToInt32(cboPlayer.SelectedValue), Convert.ToInt32(sliNormalTime.Value), Convert.ToInt32(sliInjuryTime.Value), chkIsPenalty.IsChecked, chkIsOwnGoal.IsChecked);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _result = false;
            Close();
        }

        private void UpdateGoalAppearance(int playerCode, int goalTime, int extraTime, bool? isPenalty, bool? isOwnGoal)
        {
            string goalText = "";

            if (playerCode > 0 && goalTime > 0)
            {
                //goalText = cboPlayer.SelectedItem.player_name;

                // goalText = goalText + " - " + goalTime.ToString();
                goalText = goalTime.ToString();

                if (extraTime > 0)
                {
                    goalText += "+" + extraTime.ToString() + "'";
                }
                else
                {
                    goalText += "'";
                }
                if (isPenalty == true)
                {
                    goalText += " (pen)";
                }
                if (isOwnGoal == true)
                {
                    goalText += " (og)";
                }
            }

            lblGoalAppearance.Content = goalText;
        }

        private void chkIsPenalty_Checked(object sender, RoutedEventArgs e)
        {
            UpdateGoalAppearance(Convert.ToInt32(cboPlayer.SelectedValue), Convert.ToInt32(sliNormalTime.Value), Convert.ToInt32(sliInjuryTime.Value), chkIsPenalty.IsChecked, chkIsOwnGoal.IsChecked);
        }

        private void chkIsOwnGoal_Checked(object sender, RoutedEventArgs e)
        {
            UpdateGoalAppearance(Convert.ToInt32(cboPlayer.SelectedValue), Convert.ToInt32(sliNormalTime.Value), Convert.ToInt32(sliInjuryTime.Value), chkIsPenalty.IsChecked, chkIsOwnGoal.IsChecked);
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
            if (int.TryParse(txtNormalTime.Text, out int normalTime) == true)
            {
                sliNormalTime.Value = normalTime;
            }
        }

        private void txtInjuryTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtInjuryTime.Text, out int injuryTime) == true)
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
                bool isPenalty = Convert.ToBoolean(chkIsPenalty.IsChecked);
                bool isOwnGoal = Convert.ToBoolean(chkIsOwnGoal.IsChecked);

                Goal goal = new();
                goal.GoalCode = _goalCode;
                goal.GameCode = _gameCode;
                goal.PlayerCode = Convert.ToInt32(cboPlayer.SelectedValue);
                goal.GoalTime = Convert.ToInt32(sliNormalTime.Value);
                goal.InjuryTime = Convert.ToInt32(sliInjuryTime.Value);
                goal.IsPenalty = isPenalty;
                goal.IsOwnGoal = isOwnGoal;

                GoalDataAccess da = new(_configuration);
                await da.SaveItem(goal);

                _result = true;
                Close();
            }
        }

        private bool ValidateSave()
        {
            if (Convert.ToInt32(cboPlayer.SelectedValue) == 0)
            {
                MessageBox.Show("Select player");
                return false;
            }

            if (Convert.ToInt32(sliNormalTime.Value) == 0)
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

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Goal goal = new();
            goal.GoalCode = _goalCode;

            GoalDataAccess da = new(_configuration);
            await da.DeleteItem(goal);

            _result = true;
            Close();
        }
    }
}
