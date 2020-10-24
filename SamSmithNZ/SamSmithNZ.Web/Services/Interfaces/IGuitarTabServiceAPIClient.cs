using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services.Interfaces
{
    public interface IGuitarTabServiceAPIClient
    {

        Task<List<Album>> GetAlbums();
        Task<Album> GetAlbum(int albumCode);
        Task<bool> SaveAlbum(Album item);
        Task<List<Artist>> GetArtists();
        Task<List<Rating>> GetRatings();
        Task<List<Search>> GetSearchResults();
        Task<List<Tab>> GetTabs(int albumCode, int sortOrder = 0);
        Task<Tab> GetTab(int tabCode);
        Task<bool> SaveTab(Tab item);
        Task<bool> DeleteTab(int tabCode);
        Task<List<TrackOrder>> GetTrackOrders();
        Task<List<Tuning>> GetTunings();
    }
}
