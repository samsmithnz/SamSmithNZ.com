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
    public class ShowController : ControllerBase
    {
        private readonly IShowDataAccess _repo;

        public ShowController(IShowDataAccess repo)
        {
            _repo = repo;
        }

        public async Task<List<Show>> GetShowsByYear(int yearCode)
        {
            return await _repo.GetListByYearAsync(yearCode);
        }

        public async Task<List<Show>> GetShowsBySong(int songCode)
        {
            return await _repo.GetListBySongAsync(songCode);
        }

        public async Task<Show> GetShow(int showCode)
        {
            return await _repo.GetItemAsync(showCode);
        }

    }
}
