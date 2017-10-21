using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class GameDataAccess : GenericDataAccess<Game>
    {
        public async Task<List<Game>> GetListAsync(int tournamentCode, int roundNumber, string roundCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);
            parameters.Add("@RoundCode", roundCode, DbType.String);
            parameters.Add("@IncludeGoals", true, DbType.String);

            List<Game> results = await base.GetListAsync("FB_GetGames", parameters);
            results = ProcessGameResults(results);
            return results;
        }

        public async Task<List<Game>> GetListAsyncByTeam(int teamCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TeamCode", teamCode, DbType.Int32);

            List<Game> results = await base.GetListAsync("FB_GetGames", parameters);
            results = ProcessGameResults(results);
            return results;
        }

        public async Task<List<Game>> GetListAsyncByPlayoff(int tournamentCode, int roundNumber)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);
            parameters.Add("@IncludeGoals", true, DbType.String);

            List<Game> results = await base.GetListAsync("FB_GetGames", parameters);
            results = ProcessGameResults(results);
            return results;
        }

        public async Task<List<Game>> GetListAsyncByTournament(int tournamentCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            List<Game> results = await base.GetListAsync("FB_GetGames", parameters);
            results = ProcessGameResults(results);
            return results;
        }

        //Process the game, to make it easier to process on the client side
        private List<Game> ProcessGameResults(List<Game> games)
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
                        item.Team1ResultInformation = "";
                        item.Team2ResultInformation = "";
                    }
                    else if (item.Team1ResultRegulationTimeScore < item.Team2ResultRegulationTimeScore)
                    {
                        item.Team2ResultWonGame = true;
                        item.Team1ResultWonGame = false;
                        item.Team1ResultInformation = "";
                        item.Team2ResultInformation = "";
                    }
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