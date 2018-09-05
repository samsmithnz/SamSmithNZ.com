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
    public partial class Teams : Window
    {
        private short _tournamentCode;
      
        public Teams()
        {
            InitializeComponent();
        }

        public async Task<Boolean> ShowForm(short iTournamentCode)
        {
            _tournamentCode = iTournamentCode;

            await LoadGrid(iTournamentCode);

            this.ShowDialog();
            return true;
        }

        private async Task LoadGrid(short tournamentCode)
        {
            TournamentTeamDataAccess da = new TournamentTeamDataAccess();
            List<TournamentTeam> teams = await da.GetQualifiedTeamsAsync(_tournamentCode);
            lstTeams.DataContext = teams;
        }

        private async void btnAddTeam_Click(object sender, RoutedEventArgs e)
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
            TeamDataAccess da = new TeamDataAccess();
            List<Team> teams = await da.GetListAsync();
            TournamentTeamDataAccess da2 = new TournamentTeamDataAccess();
            List<TournamentTeam> tournamentTeams = await da2.GetQualifiedTeamsAsync(_tournamentCode);

            foreach (TournamentTeam item in tournamentTeams)
            {
                Team team = teams.First(s => s.TeamCode == item.TeamCode);
                if (team != null)
                {
                    teams.RemoveAll(x => x.TeamCode == item.TeamCode);
                }
            }

            //display the teams popup            
            TeamsPopup teamsPopup = new TeamsPopup();
            int? teamCode = await teamsPopup.ShowForm(teams);

            //process the selected team
            if (teamCode != null)
            {
                TournamentTeam tournamentTeam = new TournamentTeam();
                tournamentTeam.TournamentCode = _tournamentCode;
                tournamentTeam.TeamCode = (int)teamCode;

                TournamentTeamDataAccess da3 = new TournamentTeamDataAccess();
                await da3.SaveItemAsync(tournamentTeam);
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
                TournamentTeamDataAccess da = new TournamentTeamDataAccess();
                dr.TournamentCode = _tournamentCode;
                await da.DeleteItemAsync(dr);
                await LoadGrid(_tournamentCode);
            }

        }
    }


}
