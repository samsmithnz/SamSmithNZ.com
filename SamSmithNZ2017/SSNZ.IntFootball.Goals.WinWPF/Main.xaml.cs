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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Goals.WinWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                btnTournamentGames.IsEnabled = false;
                TournamentDataAccess da = new TournamentDataAccess();
                List<Tournament> tournaments = await da.GetListAsync();
                cboTournament.DataContext = tournaments;
                cboTournament.DisplayMemberPath = "TournamentName";
                cboTournament.SelectedValuePath = "TournamentCode";
                cboTournament.SelectedIndex = 1;
                btnTournamentGames.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAssignGoals_Click(object sender, RoutedEventArgs e)
        {
            AssignGoals AGi = new AssignGoals();
            AGi.ShowDialog();
        }

        private void cboTournament_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(cboTournament.SelectedValue);
        }

        private async void btnTournamentGames_Click(object sender, RoutedEventArgs e)
        {
            if (cboTournament.SelectedValue != null)
            {
                Games Gamesi = new Games();
                await Gamesi.ShowForm(Convert.ToInt16(cboTournament.SelectedValue));
            }
            else
            {
                MessageBox.Show("Tournament Not Selected");
            }
        }



    }
}
