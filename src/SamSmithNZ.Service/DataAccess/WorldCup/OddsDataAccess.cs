using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class OddsDataAccess : BaseDataAccess<Odds>, IDataAccess<Odds>
    {
        public OddsDataAccess() : base() { }

        public async Task<List<Odds>> GetOddsForTournament(int tournamentCode)
        {
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("@TournamentCode", SqlDbType.Int) { Value = tournamentCode }
            };

            return await base.GetList("WorldCup_GetOddsForTournament", parameters);
        }

        public override Odds CreateItem(SqlDataReader reader, bool isFromStoredProcedure = true)
        {
            Odds item = new()
            {
                TeamCode = reader["TeamCode"].ToString(),
                TeamName = reader["TeamName"].ToString(),
                OddsToWin = Convert.ToDecimal(reader["OddsToWin"])
            };

            return item;
        }
    }
}
