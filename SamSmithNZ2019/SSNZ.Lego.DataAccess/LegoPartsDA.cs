using SSNZ.Lego.DataAccess.Common;
using SSNZ.Lego.DataAccess.Interfaces;
using Dapper;
using SSNZ.Lego.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Lego.DataAccess
{
    public class LegoPartsDA : GenericDataAccess<LegoParts>, ILegoPartsDA
    {
        public async Task<List<LegoParts>> GetLegoPartsAsync(string setNum)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SetNum", setNum, DbType.String);

            return await base.GetListAsync("LegoGetParts", parameters);
        }
    }
}