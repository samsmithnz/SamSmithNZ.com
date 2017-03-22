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
    public class TuningDataAccess : GenericDataAccess<Tuning>
    {
        public List<Tuning> GetItems()
        {
            return base.GetList("spKS_Tab_GetTunings").ToList<Tuning>();
        }
    }
}


