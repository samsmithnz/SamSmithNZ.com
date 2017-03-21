using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class PlayerOldDA
    {

        public Player GetDataOld(string steamID)
        {
            Player result = new Player();

            //Get Player Details
            SteamPlayerDetailDA da = new SteamPlayerDetailDA();
            SteamPlayerDetail playerDetail = da.GetDataOld(steamID);
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