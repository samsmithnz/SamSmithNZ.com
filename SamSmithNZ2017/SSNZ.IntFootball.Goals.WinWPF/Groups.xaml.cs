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
    public partial class Groups : Window
    {
        private int _tournamentCode;
        private string _groupCode;

        public Groups()
        {
            InitializeComponent();
        }

        public async Task<Boolean> ShowForm(short iTournamentCode)
        {
            _tournamentCode = iTournamentCode;

            GroupCodeDataAccess da = new GroupCodeDataAccess();
            List<GroupCode> groupCodes = await da.GetListAsync(_tournamentCode, 1);

            cboGroup.DataContext = groupCodes;
            cboGroup.DisplayMemberPath = "RoundCode";
            cboGroup.SelectedValuePath = "RoundCode";
            cboGroup.Focus();

            _groupCode = cboGroup.SelectedValue.ToString();
            await LoadGrid();

            this.ShowDialog();
            return true;
        }

        private async Task LoadGrid()
        {
            GroupDataAccess da = new GroupDataAccess();
            List<Group> groups = await da.GetListAsync(_tournamentCode, 1, _groupCode);
            lstGroups.DataContext = groups;
        }

        private async void cboGroup_Changed(object sender, SelectionChangedEventArgs e)
        {
            await LoadGrid();
        }

        private async void btnAddTeam_Click(object sender, RoutedEventArgs e)
        {
            //get the teams to pass to the popup
            List<Team> teams = new List<Team>();
            TournamentTeamDataAccess da2 = new TournamentTeamDataAccess();
            List<TournamentTeam> tournamentTeams = await da2.GetQualifiedTeamsAsync(_tournamentCode);

            foreach (TournamentTeam item in tournamentTeams)
            {
                Team team = new Team();
                team.TeamCode = item.TeamCode;
                team.TeamName = item.TeamName;
            }

            //display the teams popup            
            TeamsPopup teamsPopup = new TeamsPopup();
            int? teamCode = await teamsPopup.ShowForm(teams);

            ////process the selected team
            //if (teamCode != null)
            //{
            //    TournamentTeam tournamentTeam = new TournamentTeam();
            //    tournamentTeam.TournamentCode = _tournamentCode;
            //    tournamentTeam.TeamCode = (int)teamCode;

            //    TournamentTeamDataAccess da3 = new TournamentTeamDataAccess();
            //    await da3.SaveItemAsync(tournamentTeam);
            //    await LoadGrid(_tournamentCode);
            //}
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
            Group dr = (Group)lstGroups.ItemContainerGenerator.ItemFromContainer(dep);

            GroupDataAccess da = new GroupDataAccess();
            await da.DeleteItemAsync(dr);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private async void btnGameScoreEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    DependencyObject dep = (DependencyObject)e.OriginalSource;
        //    while ((dep != null) && !(dep is ListViewItem))
        //    {
        //        dep = VisualTreeHelper.GetParent(dep);
        //    }
        //    if (dep == null)
        //    {
        //        return;
        //    }

        //    GameGoalAssignment dr = (GameGoalAssignment)lstGroups.ItemContainerGenerator.ItemFromContainer(dep);

        //    AssignScore AssignScorei = new AssignScore();
        //    if (await AssignScorei.ShowForm(Convert.ToInt16(dr.GameCode)) == true)
        //    {
        //        await LoadGrid(_tournamentCode);
        //    }

        //}

        //private async void btnGameEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    DependencyObject dep = (DependencyObject)e.OriginalSource;
        //    while ((dep != null) && !(dep is ListViewItem))
        //    {
        //        dep = VisualTreeHelper.GetParent(dep);
        //    }
        //    if (dep == null)
        //    {
        //        return;
        //    }

        //    //dsWorldCup.GameListForGoalAssigningRow dr = (dsWorldCup.GameListForGoalAssigningRow)lstGroups.ItemContainerGenerator.ItemFromContainer(dep);
        //    GameGoalAssignment dr = (GameGoalAssignment)lstGroups.ItemContainerGenerator.ItemFromContainer(dep);

        //    Goals Goalsi = new Goals();
        //    if (await Goalsi.ShowForm(_tournamentCode, Convert.ToInt16(dr.GameCode), Convert.ToInt16(dr.TotalGoalTableGoals)) == true)
        //    {
        //        await LoadGrid(_tournamentCode);
        //    }

        //}

        //private async void btnGameShootoutEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    DependencyObject dep = (DependencyObject)e.OriginalSource;
        //    while ((dep != null) && !(dep is ListViewItem))
        //    {
        //        dep = VisualTreeHelper.GetParent(dep);
        //    }
        //    if (dep == null)
        //    {
        //        return;
        //    }

        //    //dsWorldCup.GameListForGoalAssigningRow dr = (dsWorldCup.GameListForGoalAssigningRow)lstGroups.ItemContainerGenerator.ItemFromContainer(dep);
        //    GameGoalAssignment dr = (GameGoalAssignment)lstGroups.ItemContainerGenerator.ItemFromContainer(dep);

        //    ShootoutGoals ShootoutGoalsi = new ShootoutGoals();
        //    if (await ShootoutGoalsi.ShowForm(_tournamentCode, Convert.ToInt16(dr.GameCode), Convert.ToInt16(dr.TotalPenaltyShootoutTableGoals)) == true)
        //    { //Convert.ToInt16(dr["total_game_table_penalty_shootout_goals"]) -
        //        await LoadGrid(_tournamentCode);
        //    }
        //}
    }

}
