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
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Goals.WinWPF
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

        public async Task<int?> ShowForm(List<Team> teams )
        {
            cboTeam.DataContext = teams;
            cboTeam.DisplayMemberPath = "TeamName";
            cboTeam.SelectedValuePath = "TeamCode";

            cboTeam.Focus();

            this.ShowDialog();
            return _teamCode;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _teamCode = null;
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSave() == true)
            {
                _teamCode = Convert.ToInt16(cboTeam.SelectedValue);
                this.Close();
            }
        }

        private Boolean ValidateSave()
        {
            if (Convert.ToInt16(cboTeam.SelectedValue) == 0)
            {
                MessageBox.Show("Select team");
                return false;
            }

            return true;
        }
    }


}
