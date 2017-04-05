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
    public class TuningControllerOld : ApiController
    {
        public List<Tuning> GetTunings()
        {
            TuningDataAccessOld da = new TuningDataAccessOld();
            return da.GetData();
        }
    }
}
