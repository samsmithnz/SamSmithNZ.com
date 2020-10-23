using SamSmithNZ.Service.Models.ITunes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.ITunes.Interfaces
{
    public interface ITopArtistsDataAccess
    {
        Task<List<TopArtists>> GetList(int playlistCode, bool showJustSummary);
        Task<List<TopArtists>> GetList(bool showJustSummary);
    }
}