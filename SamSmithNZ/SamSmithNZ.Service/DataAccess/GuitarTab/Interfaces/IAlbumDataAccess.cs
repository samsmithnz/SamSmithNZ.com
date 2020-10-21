using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces
{
    public interface IAlbumDataAccess
    {
        Task<List<Album>> GetList(bool isAdmin);
        Task<Album> GetItem(int albumCode, bool isAdmin);
        Task<Album> SaveItem(Album item);
    }
}