using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam2019.Service.Models
{
    public class GameDetail
    {
        public string AppID { get; set; }
        public string GameName { get; set; }
        public string IconURL { get; set; }
        public string LogoURL { get; set; }
        public int TotalAchieved { get; set; }
        public decimal PercentAchieved { get; set; }
        public int FriendTotalAchieved { get; set; }
        public decimal FriendPercentAchieved { get; set; }
        public List<Achievement> Achievements { get; set; }
        public List<KeyValuePair<string, int>> AchievementsStats { get; set; }
        public string ErrorMessage { get; set; }
    }
}
