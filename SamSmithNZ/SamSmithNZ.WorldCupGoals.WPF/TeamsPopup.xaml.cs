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
using System.Globalization;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
