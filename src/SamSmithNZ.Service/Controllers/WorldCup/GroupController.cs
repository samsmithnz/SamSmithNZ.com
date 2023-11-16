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
    public class GroupController : ControllerBase
    {
        private readonly IGroupDataAccess _repo;

        public GroupController(IGroupDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetGroups")]
        public async Task<List<Group>> GetGroups(int tournamentCode, int roundNumber, string roundCode)
        {
            return await _repo.GetList(tournamentCode, roundNumber, roundCode);
        }
    }
}
