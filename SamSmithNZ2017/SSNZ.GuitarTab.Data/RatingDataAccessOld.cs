using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SSNZ.GuitarTab.Models;
using Dapper;
using System.Data;

namespace SSNZ.GuitarTab.Data
{
    public class RatingDataAccessOld : GenericDataAccessOld<Rating>
    {
        public List<Rating> GetData()
        {
            return base.GetList("Tab_GetRatings");
        }

    }
}


