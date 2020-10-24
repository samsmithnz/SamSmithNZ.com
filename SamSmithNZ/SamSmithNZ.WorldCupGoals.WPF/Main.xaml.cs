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
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        private readonly IConfigurationRoot _configuration;

        public Main()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json");
            _configuration = config.Build();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                btnTournamentGames.IsEnabled = false;
                TournamentDataAccess da = new TournamentDataAccess(_configuration);
                List<Tournament> tournaments = await da.GetList(null);
                cboTournament.DataContext = tournaments;
                cboTournament.DisplayMemberPath = "TournamentName";
                cboTournament.SelectedValuePath = "TournamentCode";
                cboTournament.SelectedIndex = 2;
                btnTournamentGames.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AssignGoals_Click(object sender, RoutedEventArgs e)
        {
            AssignGoals AGi = new AssignGoals();
            AGi.ShowDialog();
        }

        private void TournamentDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(cboTournament.SelectedValue);
        }

        private async void TournamentGames_Click(object sender, RoutedEventArgs e)
        {
            if (cboTournament.SelectedValue != null)
            {
                Games games = new Games();
                await games.ShowForm(Convert.ToInt32(cboTournament.SelectedValue));
            }
            else
            {
                MessageBox.Show("Tournament not selected");
            }
        }

        private async void TournamentTeams_Click(object sender, RoutedEventArgs e)
        {
            if (cboTournament.SelectedValue != null)
            {
                Teams teams = new Teams();
                await teams.ShowForm(Convert.ToInt32(cboTournament.SelectedValue));
            }
            else
            {
                MessageBox.Show("Tournament not selected");
            }
        }

        private async void TournamentGroups_Click(object sender, RoutedEventArgs e)
        {
            if (cboTournament.SelectedValue != null)
            {
                Groups groups = new Groups();
                await groups.ShowForm(Convert.ToInt32(cboTournament.SelectedValue));
            }
            else
            {
                MessageBox.Show("Tournament not selected");
            }
        }

    }
}
