using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Steam2019.Service.Models
{
    public class Friend
    {
        public string SteamId { get; set; } 
        public string Name { get; set; }
        public long LastLogoff { get; set; }
        public string ProfileURL { get; set; }
        public string Avatar { get; set; }
        public string AvatarMedium { get; set; }
        public string AvatarFull { get; set; }
        public long TimeCreated { get; set; }
        public long FriendSince { get; set; }
    }
}
