using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam2019.Service.Models;
using SSNZ.Steam2019.Service.Services;

namespace SSNZ.Steam2019.Service.DataAccess
{
    public class PlayerGamesDA
    {

        public async Task<List<Game>> GetDataAsync(IRedisService redisService, string steamID)
        {
            List<Game> games = new List<Game>();

            //get games for Player
            SteamOwnedGamesDA da = new SteamOwnedGamesDA();
            SteamOwnedGames ownedGames = await da.GetDataAsync(redisService, steamID);

            //Check that the player has games to process
            if (ownedGames != null && ownedGames.response != null && ownedGames.response.games != null)
            {
                foreach (Message item in ownedGames.response.games)
                {
                    //transfer to a clean game object
                    Game newItem = new Game();
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