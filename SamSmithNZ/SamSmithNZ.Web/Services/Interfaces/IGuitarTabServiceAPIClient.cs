using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services.Interfaces
{
    public interface IGuitarTabServiceApiClient
    {

        Task<List<Album>> GetAlbums(bool isAdmin);
        Task<Album> GetAlbum(int albumCode, bool isAdmin);
        Task<bool> SaveAlbum(Album item);
        Task<List<Artist>> GetArtists(bool isAdmin);
        Task<List<Rating>> GetRatings();
        Task<List<Search>> GetSearchResults(string searchText);
        Task<List<Tab>> GetTabs(int albumCode, int sortOrder = 0);
        Task<Tab> GetTab(int tabCode);
        Task<bool> SaveTab(Tab item);
        Task<bool> DeleteTab(int tabCode);
        Task<List<TrackOrder>> GetTrackOrders();
        Task<List<Tuning>> GetTunings();
    }
}
