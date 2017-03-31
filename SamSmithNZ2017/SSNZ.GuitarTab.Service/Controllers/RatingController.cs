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
    public class RatingController : ApiController
    {
        public async Task<List<Rating>> GetRatings()
        {
            RatingDataAccess da = new RatingDataAccess();
            return await da.GetListAsync();
        }
    }
}
