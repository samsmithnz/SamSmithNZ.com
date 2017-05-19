using Dapper;
using SSNZ.Problems.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace SSNZ.Problems.Data
{
    public class SearchProblemDataAccess : GenericDataAccess<ProblemSearch>
    {
        public async Task<List<ProblemSearch>> GetListAsync(Guid recordid)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@record_id", recordid, DbType.Guid);

            return await base.GetListAsync("spProblem_GetSearchResults", parameters);
        }

        public async Task<Guid> SaveItemAsync(String searchText)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@search_text", searchText, DbType.String);

            string guidResult = await base.GetScalarAsync<string>("spProblem_SaveSearchParameters", parameters);
            return new Guid(guidResult);
        }

    }
}
