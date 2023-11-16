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
    public class TrackOrderController : ControllerBase
    {
        private readonly ITrackOrderDataAccess _repo;

        public TrackOrderController(ITrackOrderDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTrackOrders")]
        public async Task<List<TrackOrder>> GetTrackOrders()
        {
            return await _repo.GetList();
        }
    }
}
