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
    public class ArtistDataAccess : GenericDataAccess<Artist>
    {
        public async Task<List<Artist>> GetListAsync(int? includeAllItems)
        {
            DynamicParameters parameters = new DynamicParameters();
            if (includeAllItems != null)
            {
                parameters.Add("@IncludeInIndex", includeAllItems, DbType.Int32);
            }

            return await base.GetListAsync("Tab_GetArtists", parameters);
        }

    }
}


