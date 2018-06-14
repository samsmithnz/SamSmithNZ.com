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
    public partial class Games : Window
    {
        private short _tournamentCode;

        public Games()
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
            GameGoalAssignmentDataAccess da = new GameGoalAssignmentDataAccess();
            List<GameGoalAssignment> games = await da.GetListAsync(tournamentCode);
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

            AssignScore AssignScorei = new AssignScore();
            if (await AssignScorei.ShowForm(Convert.ToInt16(dr.GameCode)) == true)
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

            Goals Goalsi = new Goals();
            if (await Goalsi.ShowForm(_tournamentCode, Convert.ToInt16(dr.GameCode), Convert.ToInt16(dr.TotalGoalTableGoals)) == true)
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

            ShootoutGoals ShootoutGoalsi = new ShootoutGoals();
            if (await ShootoutGoalsi.ShowForm(_tournamentCode, Convert.ToInt16(dr.GameCode), Convert.ToInt16(dr.TotalPenaltyShootoutTableGoals)) == true)
            { //Convert.ToInt16(dr["total_game_table_penalty_shootout_goals"]) -
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
