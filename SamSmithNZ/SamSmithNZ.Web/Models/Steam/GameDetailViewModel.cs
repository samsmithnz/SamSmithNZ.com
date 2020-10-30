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
            GameDetail = gameDetail;
            if (gameDetail != null && gameDetail.Achievements != null)
            {
                TotalAchievements = gameDetail.Achievements.Count;
            }

            if (showCompletedAchievements == true)
            {
                //Only show active achievements
                List<Achievement> achievements = new List<Achievement>();
                foreach (Achievement item in gameDetail.Achievements)
                {
                    if (item.Achieved == true)
                    {
                        achievements.Add(item);
                    }
                }
                GameDetail.Achievements = achievements;
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
