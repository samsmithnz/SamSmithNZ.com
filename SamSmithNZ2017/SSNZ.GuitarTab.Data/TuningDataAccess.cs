using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SSNZ.GuitarTab.Models;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.GuitarTab.Data
{
    public class TuningDataAccess : GenericDataAccess<Tuning>
    {
        public async Task<List<Tuning>> GetDataAsync()
        {
            return await base.GetListAsync("Tab_GetTunings");
        }
    }
}


