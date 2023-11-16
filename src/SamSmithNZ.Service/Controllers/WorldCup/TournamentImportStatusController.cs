using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/WorldCup/[controller]")]
    [ApiController]
    public class TournamentImportStatusController : ControllerBase
    {
        private readonly ITournamentImportStatusDataAccess _repo;

        public TournamentImportStatusController(ITournamentImportStatusDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTournamentsImportStatus")]
        public async Task<List<TournamentImportStatus>> GetTournamentsImportStatus(int? competitionCode = null)
        {
            return await _repo.GetList(competitionCode);
        }

    }
}
