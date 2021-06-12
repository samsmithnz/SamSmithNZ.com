using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class GameDataAccess : BaseDataAccess<Game>, IGameDataAccess
    {
        public GameDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Game>> GetList(int tournamentCode, int roundNumber, string roundCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);
            parameters.Add("@RoundCode", roundCode, DbType.String);
            parameters.Add("@IncludeGoals", true, DbType.String);

            List<Game> results = await base.GetList("FB_GetGames", parameters);
            results = ProcessGameResults(results);
            return results;
        }

        public async Task<List<Game>> GetListByTeam(int teamCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TeamCode", teamCode, DbType.Int32);

            List<Game> results = await base.GetList("FB_GetGames", parameters);
            results = ProcessGameResults(results);
            return results;
        }

        public async Task<List<Game>> GetListByPlayoff(int tournamentCode, int roundNumber)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);
            parameters.Add("@IncludeGoals", true, DbType.String);

            List<Game> results = await base.GetList("FB_GetGames", parameters);
            results = ProcessGameResults(results);
            return results;
        }

        public async Task<List<Game>> GetListByTournament(int tournamentCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            List<Game> results = await base.GetList("FB_GetGames", parameters);
            results = ProcessGameResults(results);
            return results;
        }

        public async Task<Game> GetItem(int gameCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@GameCode", gameCode, DbType.Int32);

            Game result = await base.GetItem("FB_GetGames", parameters);
            List<Game> results = new()
            {
                result
            };
            results = ProcessGameResults(results);

            return results[0];
        }

        public async Task<bool> SaveItem(Game game)
        {
            DynamicParameters parameters = new();
            parameters.Add("@GameCode", game.GameCode, DbType.Int32);
            parameters.Add("@Team1NormalTimeScore", game.Team1NormalTimeScore, DbType.Int32);
            parameters.Add("@Team1ExtraTimeScore", game.Team1ExtraTimeScore, DbType.Int32);
            parameters.Add("@Team1PenaltiesScore", game.Team1PenaltiesScore, DbType.Int32);
            parameters.Add("@Team2NormalTimeScore", game.Team2NormalTimeScore, DbType.Int32);
            parameters.Add("@Team2ExtraTimeScore", game.Team2ExtraTimeScore, DbType.Int32);
            parameters.Add("@Team2PenaltiesScore", game.Team2PenaltiesScore, DbType.Int32);
            parameters.Add("@Team1StartingELORating", game.Team1PreGameEloRating, DbType.Int32);
            parameters.Add("@Team2StartingELORating", game.Team2PreGameEloRating, DbType.Int32);
            parameters.Add("@Team1EndingELORating", game.Team1PostGameEloRating, DbType.Int32);
            parameters.Add("@Team2EndingELORating", game.Team2PostGameEloRating, DbType.Int32);

            await base.SaveItem("FB_SaveGame", parameters);
            return true;
        }

        //Process the game, to make it easier to process on the client side
        private static List<Game> ProcessGameResults(List<Game> games)
        {
            foreach (Game item in games)
            {
                //if (item.GameCode == 121)
                //{
                //    int i = 4 + 4;
                //}
                //If the game didn't go to penalties
                if (item.Team1ExtraTimeScore == null && item.Team1PenaltiesScore == null)
                {
                    //Game was decided in normal time (win/draw/loss)
                    item.Team1ResultRegulationTimeScore = item.Team1NormalTimeScore;
                    item.Team2ResultRegulationTimeScore = item.Team2NormalTimeScore;
                    if (item.Team1ResultRegulationTimeScore > item.Team2ResultRegulationTimeScore)
                    {
                        item.Team1ResultWonGame = true;
                        item.Team2ResultWonGame = false;
                    }
                    else if (item.Team1ResultRegulationTimeScore < item.Team2ResultRegulationTimeScore)
                    {
                        item.Team2ResultWonGame = true;
                        item.Team1ResultWonGame = false;
                    }
                    else
                    {
                        item.Team2ResultWonGame = false;
                        item.Team1ResultWonGame = false;
                    }
                    item.Team1ResultInformation = "";
                    item.Team2ResultInformation = "";
                }
                else if (item.Team1ExtraTimeScore != null && item.Team1PenaltiesScore == null)
                {
                    //Game went to extra time (win/loss)
                    item.Team1ResultRegulationTimeScore = item.Team1NormalTimeScore + item.Team1ExtraTimeScore;
                    item.Team2ResultRegulationTimeScore = item.Team2NormalTimeScore + item.Team2ExtraTimeScore;
                    if (item.Team1ResultRegulationTimeScore > item.Team2ResultRegulationTimeScore)
                    {
                        item.Team1ResultWonGame = true;
                        item.Team2ResultWonGame = false;
                        item.Team1ResultInformation = "(aet)";
                        item.Team2ResultInformation = "";
                    }
                    else if (item.Team1ResultRegulationTimeScore < item.Team2ResultRegulationTimeScore)
                    {
                        item.Team1ResultWonGame = false;
                        item.Team2ResultWonGame = true;
                        item.Team1ResultInformation = "";
                        item.Team2ResultInformation = "(aet)";
                    }
                    else
                    {
                        item.Team1ResultWonGame = false;
                        item.Team2ResultWonGame = false;
                    }
                }

                //If the game went to penalties (win/loss)
                if (item.Team1PenaltiesScore != null)
                {
                    item.Team1ResultRegulationTimeScore = item.Team1NormalTimeScore + item.Team1ExtraTimeScore;
                    item.Team2ResultRegulationTimeScore = item.Team2NormalTimeScore + item.Team2ExtraTimeScore;
                    if (item.Team1ResultRegulationTimeScore + item.Team1PenaltiesScore > item.Team2ResultRegulationTimeScore + item.Team2PenaltiesScore)
                    {
                        item.Team1ResultWonGame = true;
                        item.Team2ResultWonGame = false;
                        item.Team1ResultInformation = "(pen)";
                        item.Team2ResultInformation = "";
                    }
                    else if (item.Team1ResultRegulationTimeScore + item.Team1PenaltiesScore < item.Team2ResultRegulationTimeScore + item.Team2PenaltiesScore)
                    {
                        item.Team1ResultWonGame = false;
                        item.Team2ResultWonGame = true;
                        item.Team1ResultInformation = "";
                        item.Team2ResultInformation = "(pen)";
                    }
                }

            }
            return games;
        }
    }
}