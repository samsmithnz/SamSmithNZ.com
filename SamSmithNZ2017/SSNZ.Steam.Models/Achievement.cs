using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam.Models
{
    public class Achievement
    {
        public string ApiName { get; set; }
        public int Achieved { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal GlobalPercent { get; set; }
        public string IconURL { get; set; }
        public string IconGrayURL { get; set; }
        public int FriendAchieved { get; set; }
    }
}
