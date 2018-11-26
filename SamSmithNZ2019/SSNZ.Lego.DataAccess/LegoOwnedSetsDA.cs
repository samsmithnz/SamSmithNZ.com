using Dapper;
using SSNZ.Lego.DataAccess.Common;
using SSNZ.Lego.DataAccess.Interfaces;
using SSNZ.Lego.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Lego.DataAccess
{
  public  class LegoOwnedSetsDA : GenericDataAccess<LegoOwnedSets>, ILegoOwnedSetsDA
    {
        public async Task<List<LegoOwnedSets>> GetLegoOwnedSetsAsync()
        {
            return await base.GetListAsync("LegoGetOwnedSets"); 
        }

        public async Task<LegoOwnedSets> GetLegoOwnedSetAsync(string setNum)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SetNum", setNum, DbType.String);

            return await base.GetItemAsync("LegoGetOwnedSets", parameters);
        }
    }
}