using SamSmithNZ.Service.Models.ITunes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.ITunes.Interfaces
{
    public interface ITrackDataAccess
    {
        Task<List<Track>> GetList(int playlistCode, bool showJustSummary);
        //Task<bool> SaveItem(Track track);
        //Task<List<Track>> ValidateTracksForPlaylist(int playlistCode);
        //Task<bool> SetTrackRanksForPlaylist(int playlistCode);
    }
}