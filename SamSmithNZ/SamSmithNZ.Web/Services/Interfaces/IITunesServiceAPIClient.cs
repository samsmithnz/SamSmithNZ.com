using SamSmithNZ.Service.Models.ITunes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services.Interfaces
{
    public interface IITunesServiceApiClient
    {
        Task<List<Movement>> GetMovementsByPlaylist(int playlistCode, bool showJustSummary);
        Task<List<Movement>> GetMovementsSummary(bool showJustSummary);
        Task<List<Playlist>> GetPlaylists(bool showJustSummary);
        Task<Playlist> GetPlaylist(int playlistCode);
        Task<List<TopArtists>> GetTopArtistsByPlaylist(int playlistCode, bool showJustSummary);
        Task<List<TopArtists>> GetTopArtistsSummary(bool showJustSummary);
        Task<List<Track>> GetTracks(int playlistCode, bool showJustSummary);
    }
}
