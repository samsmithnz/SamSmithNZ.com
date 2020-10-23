using SamSmithNZ.Service.Models.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Models.Steam
{
    public class IndexViewModel
    {
        public string SteamId { get; set; }
        public Player Player { get; set; }
        public List<Game> Games { get; set; }

        public string GetImagePath(string appID, string iconURL)
        {
            if (iconURL == null)
            {
                return "~/Steam/steam32by32.png";
            }
            else
            {
                return "http://media.steampowered.com/steamcommunity/public/images/apps/" + appID + "/" + iconURL + ".jpg";
            }
        }
    }
}
