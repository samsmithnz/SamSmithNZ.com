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
    public partial class Games : Window
    {
        private int _tournamentCode;
        private readonly IConfigurationRoot _configuration;

        public Games()
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

            await LoadGrid(tournamentCode);

            ShowDialog();
            return true;
        }

        private async Task LoadGrid(int tournamentCode)
        {
            GameGoalAssignmentDataAccess da = new(_configuration);
            List<GameGoalAssignment> games = await da.GetList(tournamentCode);
            lstGames.DataContext = games;
        }

        private async void btnGameScoreEdit_Click(object sender, RoutedEventArgs e)
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

            GameGoalAssignment dr = (GameGoalAssignment)lstGames.ItemContainerGenerator.ItemFromContainer(dep);

            AssignScore AssignScorei = new();
            if (await AssignScorei.ShowForm(Convert.ToInt32(dr.GameCode)) == true)
            {
                await LoadGrid(_tournamentCode);
            }

        }

        private async void btnGameEdit_Click(object sender, RoutedEventArgs e)
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

            //dsWorldCup.GameListForGoalAssigningRow dr = (dsWorldCup.GameListForGoalAssigningRow)lstGames.ItemContainerGenerator.ItemFromContainer(dep);
            GameGoalAssignment dr = (GameGoalAssignment)lstGames.ItemContainerGenerator.ItemFromContainer(dep);

            Goals Goalsi = new();
            if (await Goalsi.ShowForm(_tournamentCode, Convert.ToInt32(dr.GameCode), Convert.ToInt32(dr.TotalGoalTableGoals)) == true)
            {
                await LoadGrid(_tournamentCode);
            }

        }

        private async void btnGameShootoutEdit_Click(object sender, RoutedEventArgs e)
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

            //dsWorldCup.GameListForGoalAssigningRow dr = (dsWorldCup.GameListForGoalAssigningRow)lstGames.ItemContainerGenerator.ItemFromContainer(dep);
            GameGoalAssignment dr = (GameGoalAssignment)lstGames.ItemContainerGenerator.ItemFromContainer(dep);

            ShootoutGoals ShootoutGoalsi = new();
            if (await ShootoutGoalsi.ShowForm(_tournamentCode, Convert.ToInt32(dr.GameCode), Convert.ToInt32(dr.TotalPenaltyShootoutTableGoals)) == true)
            { 
                //Convert.ToInt32(dr["total_game_table_penalty_shootout_goals"]) -
                await LoadGrid(_tournamentCode);
            }
        }
    }

    [ValueConversion(typeof(object), typeof(int))]
    public class NumberToPolarValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double number = (double)System.Convert.ChangeType(value, typeof(double));
            Console.WriteLine(number);
            if (number < 0.0)
            {
                return -1;
            }

            if (number == 0.0)
            {
                return 0;
            }

            return +1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack not supported");
        }
    }

}
