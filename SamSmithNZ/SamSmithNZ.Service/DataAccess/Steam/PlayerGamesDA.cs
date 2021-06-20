using SamSmithNZ.Service.Models.Steam;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class PlayerGamesDA
    {

        public async Task<List<Game>> GetDataAsync(string steamID)
        {
            List<Game> games = new();

            //get games for Player
            SteamOwnedGamesDA da = new();
            SteamOwnedGames ownedGames = await da.GetDataAsync(steamID);

            //Check that the player has games to process
            if (ownedGames != null && ownedGames.response != null && ownedGames.response.games != null)
            {
                foreach (Message item in ownedGames.response.games)
                {
                    //transfer to a clean game object
                    Game newItem = new();
                    newItem.AppID = item.appid;
                    newItem.GameName = item.name;
                    if (string.IsNullOrEmpty(item.img_icon_url))
                    {
                        newItem.IconURL = null;
                    }
                    else
                    {
                        newItem.IconURL = item.img_icon_url;
                    }
                    newItem.LogoURL = item.img_logo_url;
                    newItem.TotalMinutesPlayed = item.playtime_forever;
                    newItem.TotalTimeString = Utility.ConvertMinutesToFriendlyTime(item.playtime_forever);
                    newItem.CommunityIsVisible = item.has_community_visible_stats;

                    games.Add(newItem);
                }
            }

            //sort the list of games by name
            games.Sort(delegate (Game p1, Game p2)
            {
                return p1.GameName.CompareTo(p2.GameName);
            });

            return games;
        }
    }
}