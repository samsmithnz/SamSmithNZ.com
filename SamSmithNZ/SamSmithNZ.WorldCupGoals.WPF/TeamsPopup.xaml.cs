using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for Games.xaml
    /// </summary>
    public partial class TeamsPopup : Window
    {
        private int? _teamCode;

        public TeamsPopup()
        {
            InitializeComponent();
        }

        public int? ShowForm(List<Team> teams )
        {
            cboTeam.DataContext = teams;
            cboTeam.DisplayMemberPath = "TeamName";
            cboTeam.SelectedValuePath = "TeamCode";

            cboTeam.Focus();

            ShowDialog();
            return _teamCode;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _teamCode = null;
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSave() == true)
            {
                _teamCode = Convert.ToInt32(cboTeam.SelectedValue);
                Close();
            }
        }

        private bool ValidateSave()
        {
            if (Convert.ToInt32(cboTeam.SelectedValue) == 0)
            {
                MessageBox.Show("Select team");
                return false;
            }

            return true;
        }
    }


}
