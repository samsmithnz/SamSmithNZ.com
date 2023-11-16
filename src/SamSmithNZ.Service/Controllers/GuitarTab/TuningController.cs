using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;

namespace SamSmithNZ.Service.Controllers.GuitarTab
{
    [Route("api/guitartab/[controller]")]
    [ApiController]
    public class TuningController : ControllerBase
    {
        private readonly ITuningDataAccess _repo;

        public TuningController(ITuningDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTunings")]
        public async Task<List<Tuning>> GetTunings()
        {
            return await _repo.GetList();
        }
    }
}
