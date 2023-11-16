using System.Collections.Generic;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class TeamMatchup
    {
        public TeamMatchup()
        {
            _games = new();
        }

        public TeamStatistics Team1Statistics { get; set; }
        public TeamStatistics Team2Statistics { get; set; }

        private List<Game> _games;

        public List<Game> Games
        {
            get
            {
                return _games;
            }
            set
            {
                _games = new();
                //Filter games to only return games that have both team1 and team2
                foreach (Game game in value)
                {
                    if ((game.Team1Code == Team1Statistics.Team.TeamCode && game.Team2Code == Team2Statistics.Team.TeamCode) ||
                        (game.Team2Code == Team1Statistics.Team.TeamCode && game.Team1Code == Team2Statistics.Team.TeamCode))
                    {
                        _games.Add(game);
                    }
                }
            }
        }



    }
}
