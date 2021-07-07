using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for Games.xaml
    /// </summary>
    public partial class Teams : Window
    {
        private int _tournamentCode;
        private readonly IConfigurationRoot _configuration;

        public Teams()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Main>(true);
            _configuration = config.Build();
        }

        public async Task<bool> ShowForm(int tournamentCode)
        {
            _tournamentCode = tournamentCode;

            await LoadGrid(tournamentCode);

            ShowDialog();
            return true;
        }

        private async Task LoadGrid(int tournamentCode)
        {
            TournamentTeamDataAccess da = new(_configuration);
            List<TournamentTeam> teams = await da.GetQualifiedTeams(tournamentCode);
            lstTeams.DataContext = teams;
            lblNumberOfTeams.Content = teams.Count.ToString() + " teams added";
        }

        private async void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            //DependencyObject dep = (DependencyObject)e.OriginalSource;
            //while ((dep != null) && !(dep is ListViewItem))
            //{
            //    dep = VisualTreeHelper.GetParent(dep);
            //}
            //if (dep == null)
            //{
            //    return;
            //}

            //get the teams to pass to the popup
            TeamDataAccess da = new(_configuration);
            List<Team> teams = await da.GetList();
            TournamentTeamDataAccess da2 = new(_configuration);
            List<TournamentTeam> tournamentTeams = await da2.GetQualifiedTeams(_tournamentCode);

            foreach (TournamentTeam item in tournamentTeams)
            {
                Team team = teams.First(s => s.TeamCode == item.TeamCode);
                if (team != null)
                {
                    teams.RemoveAll(x => x.TeamCode == item.TeamCode);
                }
            }

            //display the teams popup            
            TeamsPopup teamsPopup = new();
            int? teamCode = teamsPopup.ShowForm(teams);

            //process the selected team
            if (teamCode != null)
            {
                TournamentTeam tournamentTeam = new();
                tournamentTeam.TournamentCode = _tournamentCode;
                tournamentTeam.TeamCode = (int)teamCode;

                TournamentTeamDataAccess da3 = new(_configuration);
                await da3.SaveItem(tournamentTeam);
                await LoadGrid(_tournamentCode);
            }
        }

        private async void btnRemoveTeam_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
            {
                return;
            }

            TournamentTeam dr = (TournamentTeam)lstTeams.ItemContainerGenerator.ItemFromContainer(dep);

            if (MessageBox.Show("Are you sure you want to remove this team from the tournament?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                TournamentTeamDataAccess da = new(_configuration);
                dr.TournamentCode = _tournamentCode;
                await da.DeleteItem(dr);
                await LoadGrid(_tournamentCode);
            }

        }
    }


}
