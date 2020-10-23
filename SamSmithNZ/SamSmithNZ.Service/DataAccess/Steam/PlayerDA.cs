using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.Steam.Interfaces;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class PlayerDA
    {

        public async Task<Player> GetDataAsync(IRedisService redisService, string steamID, bool useCache)
        {
            Player result = new Player();
            
            //Get Player Details
            SteamPlayerDetailDA da = new SteamPlayerDetailDA();
            SteamPlayerDetail playerDetail = await da.GetDataAsync(redisService, steamID, useCache);
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