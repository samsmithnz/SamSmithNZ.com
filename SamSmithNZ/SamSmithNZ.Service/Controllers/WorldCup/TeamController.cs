﻿using Microsoft.AspNetCore.Mvc;
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
    public class TeamController : ControllerBase
    {
        private readonly ITeamDataAccess _repo;

        public TeamController(ITeamDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTeams")]
        public async Task<List<Team>> GetTeams()
        {
            return await _repo.GetList();
        }

        [HttpGet("GetTeam")]
        public async Task<Team> GetTeam(int teamCode)
        {
            return await _repo.GetItem(teamCode);
        }
    }
}
