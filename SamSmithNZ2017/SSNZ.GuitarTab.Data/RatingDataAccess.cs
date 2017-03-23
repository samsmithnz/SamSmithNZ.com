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
    public class RatingDataAccess : GenericDataAccess<Rating>
    {
        public async Task<List<Rating>> GetDataAsync()
        {
            return await base.GetListAsync("Tab_GetRatings");
        }

        //public List<Rating> GetDataAsync()
        //{
        //    return base.GetList("Tab_GetRatings");
        //}

    }
}


