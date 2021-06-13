using SamSmithNZ.Service.Models.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Models.Steam
{
    public class GameDetailViewModel
    {
        public GameDetailViewModel(GameDetail gameDetail, bool showCompletedAchievements)
        {
            if (gameDetail != null && gameDetail.Achievements != null)
            {
                this.TotalAchievements = gameDetail.Achievements.Count;
            }

            this.GameDetail = gameDetail;

            //Filter achievements
            if (showCompletedAchievements == false)
            {
                //Only show active achievements
                List<Achievement> achievements = new();
                foreach (Achievement item in gameDetail.Achievements)
                {
                    if (item.Achieved == false)
                    {
                        achievements.Add(item);
                    }
                }
                this.GameDetail.Achievements = achievements;
            }

        }

        public string SteamId { get; set; }
        public string AppId { get; set; }
        public Player Player { get; set; }
        public GameDetail GameDetail { get; set; }
        public int TotalAchievements { get; set; }
        public bool ShowCompletedAchievements { get; set; }

    }
}
