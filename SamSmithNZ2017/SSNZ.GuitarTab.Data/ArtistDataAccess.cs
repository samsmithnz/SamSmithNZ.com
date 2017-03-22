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
    public class ArtistDataAccess : GenericDataAccess<Artist>
    {
        public List<Artist> GetAllItems()
        {

            return base.GetList("spKS_Tab_GetArtists").ToList<Artist>();
        }

        public List<Artist> GetItems(int includeAllItems)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@includeAllItems", includeAllItems, DbType.Int32);

            return base.GetList("spKS_Tab_GetArtists", parameters).ToList<Artist>();
        }

    }
}


