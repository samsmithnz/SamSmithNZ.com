using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSNZ.Lego.DataAccess;
using SSNZ.Lego.Models;

namespace SSNZ.Lego.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegoOwnedSetsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<List<LegoOwnedSets>> Get()
        {
            LegoOwnedSetsDA da = new LegoOwnedSetsDA();
            return await da.GetLegoOwnedSetsAsync();
        }

        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
