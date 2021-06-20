using SamSmithNZ.Service.Models.Steam;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class PlayerDA
    {

        public async Task<Player> GetDataAsync(string steamID)
        {
            Player result = new();
            
            //Get Player Details
            SteamPlayerDetailDA da = new();
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