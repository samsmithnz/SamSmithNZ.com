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
    public class GroupCodeController : ControllerBase
    {
        private readonly IGroupCodeDataAccess _repo;

        public GroupCodeController(IGroupCodeDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetGroupCodes")]
        public async Task<List<GroupCode>> GetGroupCodes(int tournamentCode, int roundNumber)
        {
            return await _repo.GetList(tournamentCode, roundNumber);
        }
    }
}
