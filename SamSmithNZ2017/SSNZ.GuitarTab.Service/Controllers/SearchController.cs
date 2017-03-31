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
    public class SearchController : ApiController
    {
        public async Task<Guid> SaveSearch(string searchText)
        {
            SearchDataAccess da = new SearchDataAccess();
            return await da.SaveItemAsync(searchText);
        }

        public async Task<List<Search>> GetSearch(Guid recordId)
        {
            SearchDataAccess da = new SearchDataAccess();
            return await da.GetListAsync(recordId);
        }

        
    }
}
