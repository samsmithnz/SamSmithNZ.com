using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for AssignScore.xaml
    /// </summary>
    public partial class AssignScore : Window
    {
        private bool _bResult;
        private Game _game;
        private readonly IConfigurationRoot _configuration;

        public AssignScore()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Main>(true);
            _configuration = config.Build();
        }

        public async Task<bool> ShowForm(int gameCode)
        {
            GameDataAccess da = new(_configuration);
            _game = await da.GetItem(gameCode);

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

            //dsWorldCup.GameListForGoalAssigningDataTable dtGame = clsWCDataAccess.GameListForGoalAssigning(tournamentCode);
            //lstGames.DataContext = dtGame.DefaultView;

            ShowDialog();
            return _bResult;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _bResult = false;
            Close();
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
                    Cancel.IsEnabled = false;
                    lblStatus.Content = "Saving game...";
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

                    GameDataAccess da = new(_configuration);
                    await da.SaveItem(_game);

                    lblStatus.Content = "Updating ELO ratings...";
                    EloRatingDataAccess daELO = new(_configuration);
                    await daELO.UpdateTournamentELORatings(_game.TournamentCode);

                    lblStatus.Content = "Game updated...";
                    _bResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSave.IsEnabled = true;
                Cancel.IsEnabled = true;
            }
        }

        private bool ValidateSave()
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
                Debug.WriteLine(result);
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
            Debug.WriteLine(result);

            return true;
        }

    }
}
