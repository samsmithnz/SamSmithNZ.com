using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.WorldCup;
using SamSmithNZ.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services
{
    public class WorldCupServiceAPIClient : BaseServiceAPIClient, IWorldCupServiceAPIClient
    {
        private readonly IConfiguration _configuration;

        public WorldCupServiceAPIClient(IConfiguration configuration)
        {
            _configuration = configuration;
            HttpClient client = new()
            {
                BaseAddress = new(_configuration["AppSettings:WebServiceURL"])
            };
            base.SetupClient(client);
        }

        public async Task<bool> RefreshTournamentELORatings(int tournamentCode)
        {
            Uri url = new($"api/WorldCup/ELORating/RefreshTournamentELORatings?tournamentCode=" + tournamentCode, UriKind.Relative);
            return await base.GetMessageScalar<bool>(url);
        }

        public async Task<List<Game>> GetGames(int tournamentCode, int roundNumber, string roundCode, bool includeGoals)
        {
            Uri url = new($"api/WorldCup/Game/GetGames?tournamentCode=" + tournamentCode + "&roundNumber=" + roundNumber + "&roundCode=" + roundCode + "&includeGoals=" + includeGoals, UriKind.Relative);
            List<Game> results = await base.ReadMessageList<Game>(url);
            if (results == null)
            {
                return new();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Game>> GetGamesByTeam(int teamCode)
        {
            Uri url = new($"api/WorldCup/Game/GetGamesByTeam?teamCode=" + teamCode, UriKind.Relative);
            List<Game> results = await base.ReadMessageList<Game>(url);
            if (results == null)
            {
                return new();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Game>> GetPlayoffGames(int tournamentCode, int roundNumber, bool includeGoals)
        {
            Uri url = new($"api/WorldCup/Game/GetPlayoffGames?tournamentCode=" + tournamentCode + "&roundNumber=" + roundNumber + "&includeGoals=" + includeGoals, UriKind.Relative);
            List<Game> results = await base.ReadMessageList<Game>(url);
            if (results == null)
            {
                return new();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Game>> GetMatchUpGames(int team1Code, int team2Code)
        {
            Uri url = new($"api/WorldCup/Game/GetMatchUpGames?team1Code=" + team1Code + "&team2Code=" + team2Code, UriKind.Relative);
            List <Game> results = await base.ReadMessageList<Game>(url);
            if (results == null)
            {
                return new();
            }
            else
            {
                return results;
            }
        }

        public async Task<Game> GetGame(int gameCode)
        {
            Uri url = new($"api/WorldCup/Game/GetGame?gameCode=" + gameCode, UriKind.Relative);
            Game results = await base.ReadMessageItem<Game>(url);
            if (results == null)
            {
                return new();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<GoalInsight>> GetGoalInsights(bool analyzeExtraTime)
        {
            Uri url = new($"api/WorldCup/Insights/GetGoalInsights?analyzeExtraTime=" + analyzeExtraTime, UriKind.Relative);
            List<GoalInsight> results = await base.ReadMessageList<GoalInsight>(url);
            if (results == null)
            {
                return new List<GoalInsight>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<GroupCode>> GetGroupCodes(int tournamentCode, int roundNumber)
        {
            Uri url = new($"api/WorldCup/GroupCode/GetGroupCodes?tournamentCode=" + tournamentCode + "&roundNumber=" + roundNumber, UriKind.Relative);
            List<GroupCode> results = await base.ReadMessageList<GroupCode>(url);
            if (results == null)
            {
                return new List<GroupCode>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Group>> GetGroups(int tournamentCode, int roundNumber, string roundCode)
        {
            Uri url = new($"api/WorldCup/Group/GetGroups?tournamentCode=" + tournamentCode + "&roundNumber=" + roundNumber + "&roundCode=" + roundCode, UriKind.Relative);
            List<Group> results = await base.ReadMessageList<Group>(url);
            if (results == null)
            {
                return new List<Group>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Team>> GetTeams()
        {
            Uri url = new($"api/WorldCup/Team/GetTeams", UriKind.Relative);
            List<Team> results = await base.ReadMessageList<Team>(url);
            if (results == null)
            {
                return new List<Team>();
            }
            else
            {
                return results;
            }
        }

        public async Task<Team> GetTeam(int teamCode)
        {
            Uri url = new($"api/WorldCup/Team/GetTeam?teamCode=" + teamCode, UriKind.Relative);
            Team results = await base.ReadMessageItem<Team>(url);
            if (results == null)
            {
                return new Team();
            }
            else
            {
                return results;
            }
        }

        public async Task<TeamStatistics> GetTeamStatistics(int teamCode)
        {
            Uri url = new($"api/WorldCup/TeamStatistics/GetTeamStatistics?teamCode=" + teamCode, UriKind.Relative);
            TeamStatistics results = await base.ReadMessageItem<TeamStatistics>(url);
            if (results == null)
            {
                return new TeamStatistics();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Tournament>> GetTournaments(int competitionCode = 1)
        {
            Uri url = new($"api/WorldCup/Tournament/GetTournaments?competitionCode=" + competitionCode, UriKind.Relative);
            List<Tournament> results = await base.ReadMessageList<Tournament>(url);
            if (results == null)
            {
                return new List<Tournament>();
            }
            else
            {
                return results;
            }
        }

        public async Task<Tournament> GetTournament(int tournamentCode)
        {
            Uri url = new($"api/WorldCup/Tournament/GetTournament?tournamentCode=" + tournamentCode, UriKind.Relative);
            Tournament results = await base.ReadMessageItem<Tournament>(url);
            if (results == null)
            {
                return new Tournament();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<TournamentTeam>> GetTournamentQualifyingTeams(int tournamentCode)
        {
            Uri url = new($"api/WorldCup/TournamentTeam/GetTournamentQualifyingTeams?tournamentCode=" + tournamentCode, UriKind.Relative);
            List<TournamentTeam> results = await base.ReadMessageList<TournamentTeam>(url);
            if (results == null)
            {
                return new List<TournamentTeam>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<TournamentTeam>> GetTournamentPlacingTeams(int tournamentCode)
        {
            Uri url = new($"api/WorldCup/TournamentTeam/GetTournamentPlacingTeams?tournamentCode=" + tournamentCode, UriKind.Relative);
            List<TournamentTeam> results = await base.ReadMessageList<TournamentTeam>(url);
            if (results == null)
            {
                return new List<TournamentTeam>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<TournamentTopGoalScorer>> GetTournamentTopGoalScorers(int tournamentCode, bool getOwnGoals)
        {
            Uri url = new($"api/WorldCup/TournamentTopGoalScorer/GetTournamentTopGoalScorers?tournamentCode=" + tournamentCode + "&getOwnGoals=" + getOwnGoals, UriKind.Relative);
            List<TournamentTopGoalScorer> results = await base.ReadMessageList<TournamentTopGoalScorer>(url);
            if (results == null)
            {
                return new List<TournamentTopGoalScorer>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Playoff>> GetPlayoffSetup(int tournamentCode)
        {
            Uri url = new($"api/WorldCup/Playoff/GetPlayoffs?tournamentCode=" + tournamentCode, UriKind.Relative);
            List<Playoff> results = await base.ReadMessageList<Playoff>(url);
            if (results == null)
            {
                return new List<Playoff>();
            }
            else
            {
                return results;
            }
        }

    }
}
