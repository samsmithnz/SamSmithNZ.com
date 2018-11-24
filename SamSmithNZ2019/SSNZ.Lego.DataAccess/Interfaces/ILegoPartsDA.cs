using SSNZ.Lego.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.Lego.DataAccess.Interfaces
{
    interface ILegoPartsDA
    {
        Task<List<LegoParts>> GetLegoPartsAsync(string setNum);
    }
}
