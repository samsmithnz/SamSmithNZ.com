using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;

namespace SSNZ.GuitarTab.Service.Controllers
{
    public class TrackOrderController : ApiController
    {
        public async Task<List<TrackOrder>> GetTrackOrders()
        {
            TrackOrderDataAccess da = new TrackOrderDataAccess();
            return await da.GetListAsync();
        }
    }
}
