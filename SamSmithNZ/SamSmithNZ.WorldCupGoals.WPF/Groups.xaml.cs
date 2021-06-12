﻿using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for Games.xaml
    /// </summary>
    public partial class Groups : Window
    {
        private int _tournamentCode;
        private string _groupCode;
        private readonly IConfigurationRoot _configuration;

        public Groups()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json");
            _configuration = config.Build();
        }

        public async Task<bool> ShowForm(int tournamentCode)
        {
            _tournamentCode = tournamentCode;

            GroupCodeDataAccess da = new GroupCodeDataAccess(_configuration);
            List<GroupCode> groupCodes = await da.GetList(_tournamentCode, 1);

            cboGroup.DataContext = groupCodes;
            cboGroup.DisplayMemberPath = "RoundCode";
            cboGroup.SelectedValuePath = "RoundCode";
            cboGroup.Focus();
            cboGroup.SelectedIndex = 0;

            _groupCode = cboGroup.SelectedValue.ToString();
            await LoadGrid();

            ShowDialog();
            return true;
        }

        private async Task LoadGrid()
        {
            GroupDataAccess da = new GroupDataAccess(_configuration);
            List<Group> groups = await da.GetList(_tournamentCode, 1, _groupCode);
            lstGroups.DataContext = groups;
        }

        private async void cboGroup_Changed(object sender, SelectionChangedEventArgs e)
        {
            _groupCode = cboGroup.SelectedValue.ToString();
            await LoadGrid();
        }

        private async void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            //get the teams to pass to the popup
            List<Team> teams = new List<Team>();
            TournamentTeamDataAccess da2 = new TournamentTeamDataAccess(_configuration);
            List<TournamentTeam> tournamentTeams = await da2.GetQualifiedTeams(_tournamentCode);

            foreach (TournamentTeam item in tournamentTeams)
            {
                Team team = new Team();
                team.TeamCode = item.TeamCode;
                team.TeamName = item.TeamName;
            }

            //display the teams popup            
            TeamsPopup teamsPopup = new TeamsPopup();
            int? teamCode = teamsPopup.ShowForm(teams);
            Debug.WriteLine(teamCode);
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

            GroupDataAccess da = new GroupDataAccess(_configuration);
            await da.DeleteItemAsync(dr);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        //    if (await AssignScorei.ShowForm(Convert.ToInt32(dr.GameCode)) == true)
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
        //    if (await Goalsi.ShowForm(_tournamentCode, Convert.ToInt32(dr.GameCode), Convert.ToInt32(dr.TotalGoalTableGoals)) == true)
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
        //    if (await ShootoutGoalsi.ShowForm(_tournamentCode, Convert.ToInt32(dr.GameCode), Convert.ToInt32(dr.TotalPenaltyShootoutTableGoals)) == true)
        //    { //Convert.ToInt32(dr["total_game_table_penalty_shootout_goals"]) -
        //        await LoadGrid(_tournamentCode);
        //    }
        //}
    }

}
