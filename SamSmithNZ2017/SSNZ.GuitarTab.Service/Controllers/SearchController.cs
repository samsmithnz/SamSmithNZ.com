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
        public async Task<List<Search>> GetSearchResults(string searchText)
        {
            SearchDataAccess da = new SearchDataAccess();
            Guid recordId =  await da.SaveItemAsync(searchText);

            return await da.GetListAsync(recordId);
        }
        
    }
}
