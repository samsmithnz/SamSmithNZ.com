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
    public class ArtistController : ApiController
    {
        public async Task<List<Artist>> GetArtists(bool includeAllItems) 
        {
            ArtistDataAccess da = new ArtistDataAccess();
            return await da.GetListAsync(includeAllItems);
        }
    }
}
