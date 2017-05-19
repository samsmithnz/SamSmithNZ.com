using Dapper;
using SSNZ.Problems.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.Problems.Data
{
    public class ProblemsDataAccess : GenericDataAccess<Problem>
    {
        public async Task<List<Problem>> GetListAsync()
        {
            DynamicParameters parameters = new DynamicParameters();

            return await base.GetListAsync("spProblem_GetProblems", parameters);
        }

        public async Task<Problem> GetItemAsync(int problemNumber)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@problem_number", problemNumber, DbType.Int32);

            return await base.GetItemAsync("spProblem_GetProblems", parameters);
        }

        public async Task<bool> SaveItemAsync(int problemNumber, string description, string notes, bool isCompleted)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@problem_number", problemNumber, DbType.Int32);
            parameters.Add("@description", description, DbType.String);
            parameters.Add("@notes", notes, DbType.String);
            parameters.Add("@is_completed", isCompleted, DbType.Boolean);

            return await base.GetScalarAsync<bool>("spProblem_SaveProblem", parameters);
        }

    }
}


