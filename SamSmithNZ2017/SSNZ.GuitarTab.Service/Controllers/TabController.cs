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
    public class TabController : ApiController
    {
        public async Task<List<Tab>> GetTabs(int albumCode)
        {
            TabDataAccess da = new TabDataAccess();
            return await da.GetListAsync(albumCode);
        }

        public async Task<Tab> GetTab(int trackCode)
        {
            TabDataAccess da = new TabDataAccess();
            return await da.GetItemAsync(trackCode);
        }

        public async Task<bool> SaveTab(Tab item)
        {
            TabDataAccess da = new TabDataAccess();
            return await da.SaveItemAsync(item);
        }

        public async Task<bool> DeleteTab(int trackCode)
        {
            TabDataAccess da = new TabDataAccess();
            return await da.DeleteItemAsync(trackCode);
        }
    }
}
