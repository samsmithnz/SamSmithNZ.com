using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam2019.Service.Models;

namespace SSNZ.Steam2019.Service.DataAccess
{
    public class PlayerDA
    {

        public async Task<Player> GetDataAsync(string steamID)
        {
            Player result = new Player();
            
            //Get Player Details
            SteamPlayerDetailDA da = new SteamPlayerDetailDA();
            SteamPlayerDetail playerDetail = await da.GetDataAsync(steamID);
            if (playerDetail == null)
            {
                return null; // RedirectToAction("SteamIsDown", "Steam");
            }
            else
            {
                //If detail exists, then load the details into a clean object
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