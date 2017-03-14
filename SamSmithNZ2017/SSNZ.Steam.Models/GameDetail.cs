using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam.Models
{
    public class GameDetail
    {
        public string AppID { get; set; }
        public string GameName { get; set; }
        public int TotalArchieved { get; set; }
        public decimal PercentAchieved { get; set; }
        public List<Achievement> Achievements { get; set; }
    }
}
