﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SSNZ.GuitarTab.Models;
using Dapper;
using System.Data;

namespace SSNZ.GuitarTab.Data
{
    public class ArtistDataAccessOld : GenericDataAccessOld<Artist>
    {
        public List<Artist> GetData(bool? includeAllItems)
        {
            DynamicParameters parameters = new DynamicParameters();
            if (includeAllItems != null)
            {
                parameters.Add("@IncludeInIndex", includeAllItems, DbType.Boolean);
            }

            return base.GetList("Tab_GetArtists", parameters).ToList<Artist>();
        }

    }
}


