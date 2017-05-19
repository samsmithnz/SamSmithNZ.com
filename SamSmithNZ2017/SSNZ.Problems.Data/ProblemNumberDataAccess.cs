using Dapper;
using SSNZ.Problems.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.Problems.Data
{
    public class ProblemNumberDataAccess : GenericDataAccess<ProblemNumber>
    {
        public async Task<List<ProblemNumber>> GetListAsync()
        {
            DynamicParameters parameters = new DynamicParameters();
            
            return await base.GetListAsync("spProblem_GetProblemNumberList", parameters);
        }
        
    }
}