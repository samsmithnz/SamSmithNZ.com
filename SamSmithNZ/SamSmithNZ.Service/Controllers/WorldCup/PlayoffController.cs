using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/WorldCup/[controller]")]
    [ApiController]
    public class PlayoffController : ControllerBase
    {
        private readonly IPlayoffDataAccess _repo;

        public PlayoffController(IPlayoffDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetPlayoffs")]
        public async Task<List<Playoff>> GetPlayoffs(int tournamentCode)
        {
            return await _repo.GetList(tournamentCode);
        }
    }
}
