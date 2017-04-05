﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;

namespace SSNZ.GuitarTab.Service.Controllers
{
    public class SearchControllerOld : ApiController
    {
        public Guid SaveSearch(string searchText)
        {
            SearchDataAccessOld da = new SearchDataAccessOld();
            return  da.SaveItem(searchText);
        }

        public List<Search> GetSearch(Guid recordId)
        {
            SearchDataAccessOld da = new SearchDataAccessOld();
            return  da.GetData(recordId);
        }

        
    }
}
