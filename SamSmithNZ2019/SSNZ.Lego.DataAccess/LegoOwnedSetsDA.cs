using SSNZ.Lego.DataAccess.Common;
using SSNZ.Lego.DataAccess.Interfaces;
using SSNZ.Lego.Models;
using System;
using System.Collections.Generic;
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
    }
}