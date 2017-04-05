using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;

namespace SSNZ.GuitarTab.Service.Controllers
{
    public class TabOldController : ApiController
    {
        public List<Tab> GetTabs(int albumCode)
        {
            TabDataAccessOld da = new TabDataAccessOld();
            return  da.GetData(albumCode);
        }

        public Tab GetTab(int tabCode)
        {
            TabDataAccessOld da = new TabDataAccessOld();
            return  da.GetItem(tabCode);
        }

        public bool SaveTab(Tab item)
        {
            TabDataAccessOld da = new TabDataAccessOld();
            return  da.SaveItem(item);
        }

        public bool DeleteTab(int tabCode)
        {
            TabDataAccessOld da = new TabDataAccessOld();
            return  da.DeleteItem(tabCode);
        }
    }
}
