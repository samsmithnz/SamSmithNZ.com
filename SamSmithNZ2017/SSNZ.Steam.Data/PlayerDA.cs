using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class PlayerDA
    {

        public Player GetData(string steamID)
        {
            Player result = new Player();

            //Get Player Details
            SteamPlayerDetailDA da = new SteamPlayerDetailDA();
            SteamPlayerDetail playerDetail = da.GetData(steamID);
            if (playerDetail == null) 
            {
                return null; // RedirectToAction("SteamIsDown", "Steam");
            }
            else
            {
                if (playerDetail.response.players.Count > 0)
                {
                    result.SteamID = steamID;
                    result.PlayerName = playerDetail.response.players[0].personaname;
                    if (playerDetail.response.players[0].communityvisibilitystate == 3)
                    {
                        result.IsPublic = true;
                    }
                }
            
                return result;
            }
        }
    }
}