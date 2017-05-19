using Dapper;
using SSNZ.ITunes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.ITunes.Data
{
    public class MovementDataAccess : GenericDataAccess<Movement>
    {

        public async Task<List<Movement>> GetListAsync(int playlistCode, Boolean showJustSummary)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@playlist_code", playlistCode, DbType.Int32);
            parameters.Add("@show_just_summary", showJustSummary, DbType.Boolean);

            return await base.GetListAsync("spITunes_GetMovement", parameters);
        }        

        public async Task<List<Movement>> GetListAsyncByTournament(Boolean showJustSummary)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@show_just_summary", showJustSummary, DbType.Boolean);

            return await base.GetListAsync("spITunes_GetMovement", parameters);
        }        
        
    }
}