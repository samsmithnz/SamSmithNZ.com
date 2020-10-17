using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;

namespace SamSmithNZ.Service.Controllers.FooFighters
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearController : ControllerBase
    {
        private readonly IYearDataAccess _repo;

        public YearController(IYearDataAccess repo)
        {
            _repo = repo;
        }

        public async Task<List<Year>> GetYears()
        {
            return await _repo.GetListAsync();
        }

    }
}
