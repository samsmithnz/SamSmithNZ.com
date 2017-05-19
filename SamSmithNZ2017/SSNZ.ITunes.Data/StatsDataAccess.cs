//using Dapper;
//using SSNZ.ITunes.Models;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Threading.Tasks;

//namespace SSNZ.ITunes.Data
//{
//    public class StatsDataAccess : GenericDataAccess<Stats>
//    {
//        public async Task<List<Game>> GetListAsyncByTournament(int tournamentCode)
//        {
//            DynamicParameters parameters = new DynamicParameters();
//            parameters.Add("@tournament_code", tournamentCode, DbType.Int32);

//            return await base.GetListAsync("spITunes_GetStats", parameters);
//        }
//        public Stats GetItem()
//        {
//            SQLQueryContext queryContext = GetQueryContext("spITunes_GetStats");

//            return base.Retrieve(queryContext);
//        }

       
//    }
//}
