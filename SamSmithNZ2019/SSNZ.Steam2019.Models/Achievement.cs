using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam2019.Models
{
    public class Achievement
    {
        public string ApiName { get; set; }
        public bool Achieved { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal GlobalPercent { get; set; }
        public string IconURL { get; set; }
        public string IconGrayURL { get; set; }
        public bool FriendAchieved { get; set; }
        public bool IsVisible { get; set; }
    }
}
