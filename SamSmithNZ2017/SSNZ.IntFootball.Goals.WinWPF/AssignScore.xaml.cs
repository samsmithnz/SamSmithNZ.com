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
    /// Interaction logic for AssignScore.xaml
    /// </summary>
    public partial class AssignScore : Window
    {
        private Boolean _bResult;
        private Game _game;

        public AssignScore()
        {
            InitializeComponent();
        }

        public async Task<Boolean> ShowForm(int gameCode)
        {
            GameDataAccess da = new GameDataAccess();
            _game = await da.GetItemAsync(gameCode);

            lblGameHeader.Content = "#" + _game.GameNumber + ": " + _game.GameTime.ToString("dd-MMM-yyyy hh:mm:sstt");
            lblGame.Content = _game.Team1Name + " vs " + _game.Team2Name;
            lblStatus.Content = "";

            txtTeam1NormalTime.Text = _game.Team1NormalTimeScore.ToString();
            txtTeam1ExtraTime.Text = _game.Team1ExtraTimeScore.ToString();
            txtTeam1Penalties.Text = _game.Team1PenaltiesScore.ToString();
            txtTeam2NormalTime.Text = _game.Team2NormalTimeScore.ToString();
            txtTeam2ExtraTime.Text = _game.Team2ExtraTimeScore.ToString();
            txtTeam2Penalties.Text = _game.Team2PenaltiesScore.ToString();

            txtTeam1NormalTime.Focus();

            //dsWorldCup.GameListForGoalAssigningDataTable dtGame = clsWCDataAccess.GameListForGoalAssigning(iTournamentCode);
            //lstGames.DataContext = dtGame.DefaultView;

            this.ShowDialog();
            return _bResult;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _bResult = false;
            this.Close();
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
            try
            {
                if (ValidateSave() == true)
                {
                    btnSave.IsEnabled = false;
                    btnCancel.IsEnabled = false;
                    lblStatus.Content = "Updating ELO ratings...";
                    if (txtTeam1NormalTime.Text.Length == 0 || txtTeam2NormalTime.Text.Length == 0)
                    {
                        _game.Team1NormalTimeScore = null;
                        _game.Team2NormalTimeScore = null;
                        _game.Team1ExtraTimeScore = null;
                        _game.Team2ExtraTimeScore = null;
                        _game.Team1PenaltiesScore = null;
                        _game.Team2PenaltiesScore = null;
                    }
                    else
                    {
                        _game.Team1NormalTimeScore = Convert.ToInt32(txtTeam1NormalTime.Text);
                        _game.Team2NormalTimeScore = Convert.ToInt32(txtTeam2NormalTime.Text);
                        if (txtTeam1ExtraTime.Text.Length > 0 || txtTeam2ExtraTime.Text.Length > 0)
                        {
                            _game.Team1ExtraTimeScore = Convert.ToInt32(txtTeam1ExtraTime.Text);
                            _game.Team2ExtraTimeScore = Convert.ToInt32(txtTeam2ExtraTime.Text);
                        }
                        else
                        {
                            _game.Team1ExtraTimeScore = null;
                            _game.Team2ExtraTimeScore = null;
                        }
                        if (txtTeam1Penalties.Text.Length > 0 || txtTeam2Penalties.Text.Length > 0)
                        {
                            _game.Team1PenaltiesScore = Convert.ToInt32(txtTeam1Penalties.Text);
                            _game.Team2PenaltiesScore = Convert.ToInt32(txtTeam2Penalties.Text);
                        }
                        else
                        {
                            _game.Team1PenaltiesScore = null;
                            _game.Team2PenaltiesScore = null;
                        }
                    }

                    GameDataAccess da = new GameDataAccess();
                    await da.SaveItemAsync(_game);

                    EloRatingDataAccess daELO = new EloRatingDataAccess();
                    await daELO.UpdateTournamentELORatings(_game.TournamentCode);

                    lblStatus.Content = "Score saved...";
                    _bResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSave.IsEnabled = true;
                btnCancel.IsEnabled = true;
            }
        }

        private Boolean ValidateSave()
        {
            int result;
            if (txtTeam1NormalTime.Text.Length == 0 && txtTeam2NormalTime.Text.Length == 0)
            {
                if (MessageBox.Show("Are you sure you want to remove this result?", "Remove result?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (int.TryParse(txtTeam1NormalTime.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 1 normal time score");
                return false;
            }
            if (int.TryParse(txtTeam2NormalTime.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 2 normal time score");
                return false;
            }
            if (txtTeam1ExtraTime.Text.Length > 0 && int.TryParse(txtTeam1ExtraTime.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 1 extra time score");
                return false;
            }
            if (txtTeam2ExtraTime.Text.Length > 0 && int.TryParse(txtTeam2ExtraTime.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 2 extra time score");
                return false;
            }

            if (txtTeam1Penalties.Text.Length > 0 && int.TryParse(txtTeam1Penalties.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 1 penalties score");
                return false;
            }
            if (txtTeam2Penalties.Text.Length > 0 && int.TryParse(txtTeam2Penalties.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 2 penalties score");
                return false;
            }

            return true;
        }

    }
}
