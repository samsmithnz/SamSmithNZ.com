using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class PlayerGamesDA
    {

        public List<Game> GetData(string steamID)
        {
            List<Game> games = new List<Game>();

            //get games for Player
            SteamOwnedGamesDA da = new SteamOwnedGamesDA();
            SteamOwnedGames ownedGames = da.GetData(steamID);
            foreach (Message item in ownedGames.response.games)
            {
                Game newItem = new Game();
                newItem.AppID = item.appid;
                newItem.GameName = item.name;
                newItem.IconURL = item.img_icon_url;
                newItem.LogoURL = item.img_logo_url;
                newItem.TotalMinutesPlayed = item.playtime_forever;
                newItem.TotalTimeString = Utility.ConvertMinutesToFriendlyTime(item.playtime_forever);
                newItem.CommunityIsVisible = item.has_community_visible_stats;
                games.Add(newItem);
            }

            games.Sort(delegate (Game p1, Game p2)
            {
                return p1.GameName.CompareTo(p2.GameName);
            });

            return games;
        }
    }
}